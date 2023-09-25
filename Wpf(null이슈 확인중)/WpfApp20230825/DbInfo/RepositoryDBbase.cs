using MssqlLib;

namespace WpfApp20230825.DbInfo
{
    public abstract class RepositoryDBbase
    {
        public Mssql Getdb()
        {
            //서버정보 나중에 감추기.
            string connectionString = "Server=동환\\SQLEXPRESS;Database=UserDb;User Id=tester1;Password=password11;";
            return new Mssql(connectionString);
        }
    }
}
