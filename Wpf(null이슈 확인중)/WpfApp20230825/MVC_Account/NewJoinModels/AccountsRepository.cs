using MssqlLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using WpfApp20230825.DbInfo;

namespace WpfApp20230825.MVC_Account.NewJoinModels
{
    public class AccountsRepository : RepositoryDBbase, IAccountsRepository
    {
        private List<Accounts> GetAll()
        {
            List<Accounts> list = new List<Accounts>();
            using (Mssql? db = Getdb())
            {
                string query = "SELECT * FROM ACCOUNTS";
                using (IDataReader dr = db.ExecuteReader(query))
                {
                    while (dr.Read())
                    {
                        Accounts account = new Accounts()
                        {
                            Id = (string)dr["Id"],
                            Pw = (string)dr["Pw"],
                            StudentName = (string)dr["StudentName"]
                        };
                        list.Add(account);
                    }
                }//괄호 제거시 이곳에 dr.Dispose(); 호출
            }//괄호 제거시 이곳에 db.Dipose(); 호출
            return list;
        }
        public bool isValid(string Id)
        {
            try
            {
                List<Accounts> accounts = GetAll();
                foreach (Accounts account in accounts)
                {
                    if (account.Id.Equals(Id))
                    {
                        MessageBox.Show("동일한 아이디가 있습니다.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Save(Accounts accounts)
        {
            if (isValid(accounts.Id))
            {
                string a = accounts.Id;
                string b = accounts.Pw;
                string c = accounts.StudentName;
                string query = $"INSERT INTO Accounts (Id, Pw, StudentName) VALUES (@Id, @Pw, @StudentName)";
                using (Mssql? db = Getdb())
                {
                    db.AccountExecute(query, a, b, c);
                    MessageBox.Show("Account db 에서 Insert 쿼리문을 실행중입니다.");
                    return true;
                }
                //괄호 제거시 이곳에 dr.Dispose(); 호출
            }//괄호 제거시 이곳에 db.Dipose(); 호출
            else return false;
        }

        //로그인정보 studentrepository에 넘길 때
        public Accounts Check_Account(string id, string pw)
        {
            Accounts ac = new Accounts();
            List<Accounts> checkAccounts = GetAll();

            foreach (Accounts account in checkAccounts)
            {
                if (account.Id.Equals(id) && account.Pw.Equals(pw))
                {
                    ac = account;
                    return ac;
                }
            }
            return ac;
        }

    }
}




