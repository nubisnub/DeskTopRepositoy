using WpfApp20230825.MVC_Account.NewJoinModels;
using WpfApp20230825.MVC_Account.NewjoinViews;

namespace WpfApp20230825.MVC_Account.NewJoinControllers
{
    public class AccountsController
    {
        private readonly INewJoinPage nJP;
        private readonly IAccountsRepository accountsRepository;
        private readonly MainWindow mw;

        private Accounts GetAccounts()
        {
            
            return new Accounts
            {
                Id = nJP.Id,
                Pw = nJP.Pw,
                StudentName = nJP.StudentName,
            };
        }
        public AccountsController(INewJoinPage nJP, IAccountsRepository accountsRepository)
        {
            this.nJP = nJP;
            this.accountsRepository = accountsRepository;
            nJP.SetController(this);
        }
        public AccountsController(MainWindow mainwindow, AccountsRepository accountsRepository)
        {
            this.mw = mainwindow;
            this.accountsRepository = accountsRepository;
        }

        public string PasswordConvert(string password)
        {
            if (password == null)
            {
                return "";
            }
            else
            {

                return password;
            };
        }

        public bool SaveControl()
        {
            Accounts accounts = GetAccounts();
            if (accountsRepository.isValid(nJP.Id))
            {
                accountsRepository.Save(accounts);

                return true;
            }

            return false;
        }
    }
}
