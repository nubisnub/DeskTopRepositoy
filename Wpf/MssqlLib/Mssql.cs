using System.Data.SqlClient;

namespace MssqlLib
{
    public class Mssql : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection? _connection;



        private void OpenConnection()
        {
            _connection = new SqlConnection(_connectionString);
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Mssql(string connectionString)
        {
            _connectionString = connectionString;
            OpenConnection();
        }

        //작업할 내용. StudentDB 관련 실행문
        public void StudentDBExecute(String query, string Name, DateTime Day_Of_The_Classhe_Class, int ReviewRatio)
        {
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@ReviewRatio", ReviewRatio);
                cmd.Parameters.AddWithValue("@Day_Of_The_Class", Day_Of_The_Classhe_Class);
                cmd.ExecuteNonQuery();
            }
        }




        //작업할 내용 Account한정 execute 구문... Sqlparameter 이쁘게 짜서 코드 간결화 하기.
        public void AccountExecute(String query, string Id, string Pw, string studenName)
        {
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Pw", Pw);
                cmd.Parameters.AddWithValue("@StudentName", studenName);
                cmd.ExecuteNonQuery();
            }
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _connection);
            return cmd.ExecuteReader();
        }

        //Admin 용    
        public string SelectQuery()
        {
            //StudentDB 테이블에는 3가지 열이 있어 Name, Day_Of_The_Class, ReviewRatio이 존재             
            return "SELECT * FROM StudentDB";
        }

        //회원용
        public string SelectQuery(string AccountStudentName)
        {
            //StudentDB 테이블에는 3가지 열이 있어 Name, Day_Of_The_Class, ReviewRatio이 존재             
            return $"SELECT * FROM StudentDB WHERE Name = '{AccountStudentName}'";
        }



        //괄호 제거시
        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
  
    }
}