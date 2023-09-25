namespace WpfApp20230825.MVC_Account.NewJoinModels
{
    public interface IAccountsRepository
    {
        public bool Save(Accounts accounts);
        public bool isValid(string Id);

    }
}