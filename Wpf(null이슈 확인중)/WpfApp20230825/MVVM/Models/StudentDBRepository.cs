using MssqlLib;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using WpfApp20230825.DbInfo;
using WpfApp20230825.MVC_Account.NewJoinModels;

namespace WpfApp20230825.Models
{
    public class StudentDBRepository : RepositoryDBbase, IStudentDBRepository
    {//Name, Day_Of_The_Class, ReviewRatio

        public Accounts AccountInfo { get; set; } = default!;


        public bool InsertOrUpdate(StudentDB studentDB)
        {
            try
            {
                using (Mssql? db = Getdb())
                {
                    var existingStudent = GetOne().FirstOrDefault(s => s.Day_Of_The_Class.Equals(studentDB.Day_Of_The_Class));

                    if (existingStudent != null)
                    {
                        MessageBox.Show("현재 studentrepoistoy에서 Update를 실행중입니다");
                        string updateQuery = "UPDATE StudentDB SET ReviewRatio = @ReviewRatio WHERE Day_Of_The_Class = @Day_Of_The_Class";
                        db.StudentDBExecute(updateQuery, existingStudent.Name, existingStudent.Day_Of_The_Class, studentDB.ReviewRatio);
                    }

                    else
                    {
                        MessageBox.Show("현재 studentrepoistoy에서 Insert를 실행중입니다");
                        string insertQuery = "INSERT INTO StudentDB (Name, Day_Of_The_Class, ReviewRatio) VALUES (@Name, @Day_Of_The_Class, @ReviewRatio)";
                        db.StudentDBExecute(insertQuery, studentDB.Name, studentDB.Day_Of_The_Class, studentDB.ReviewRatio);
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("저장 및 수정에 오류가 발생하였습니다.");
                return false;
            }
        }

        //이름과 수업일만 맞으면 삭제해도 됌(오기이기 때문에)
        public bool Delete(string name, string day_of_the_class)
        {
            if (!DateTime.TryParseExact(day_of_the_class, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                MessageBox.Show("올바른 형식이 아닙니다");
                return false;
            }
            string query = $"DELETE FROM StudentDB WHERE Day_Of_The_Class = @day_of_the_class AND Name = '{name}'";
            try
            {
                using (Mssql? db = Getdb())
                {
                    db.StudentDBExecute(query, "", result, 0);
                    MessageBox.Show("Delete를 하였습니다.");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }


        //회원 학생 조회용
        public ObservableCollection<StudentDB> GetOne()
        {
            ObservableCollection<StudentDB> a_student_Review_State = new ObservableCollection<StudentDB>();
            using Mssql? mssql = Getdb();

            using (IDataReader dr = mssql.ExecuteReader(mssql.SelectQuery(AccountInfo.StudentName)))
            {
                while (dr.Read())
                {
                    StudentDB studentDB = new StudentDB()
                    {
                        Name = (string)dr["Name"],
                        Day_Of_The_Class = (DateTime)dr["Day_Of_The_Class"],
                        ReviewRatio = (int)dr["ReviewRatio"]
                    };
                    a_student_Review_State.Add(studentDB);
                }
            }
            return a_student_Review_State;
        }


        //Admin 조회용
        public ObservableCollection<StudentDB> GetAll()
        {
            ObservableCollection<StudentDB> All_student_State = new ObservableCollection<StudentDB>();
            using Mssql? mssql = Getdb();

            using (IDataReader dr = mssql.ExecuteReader(mssql.SelectQuery()))
            {
                while (dr.Read())
                {
                    StudentDB studentDB = new StudentDB()
                    {
                        Name = (string)dr["Name"],
                        Day_Of_The_Class = (DateTime)dr["Day_Of_The_Class"],
                        ReviewRatio = (int)dr["ReviewRatio"]
                    };
                    All_student_State.Add(studentDB);

                }
            }
            return All_student_State;
        }



    }
}
