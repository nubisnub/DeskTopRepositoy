using System.Windows;
using System.Windows.Controls;
using WpfApp20230825.MVC_Account.NewJoinControllers;
using WpfApp20230825.MVC_Account.NewJoinModels;
using WpfApp20230825.MVC_Account.NewjoinViews;

namespace WpfApp20230825
{
    /// <summary>
    /// NewJoinPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewJoinPage : Window, INewJoinPage
    {
        private AccountsController? _accountsController = default!;
        
        public NewJoinPage()
        {
            InitializeComponent();

        }

        private void MemberJoin_Clicked(object sender, RoutedEventArgs e)
        {
            if (_accountsController != null)
            {
            Pw = _accountsController.PasswordConvert(NJP_password.Password)!;
            if (_accountsController.SaveControl())
            {
                MessageBox.Show("가입이 완료되었습니다. 로그인하세요");
                Close();
            }
            }
        }

        public void SetController(AccountsController controller)
        {
            _accountsController = controller;
        }

        public string Id
        {
            get
            {
                return Account_Id.Text;
            }
            set
            {
                Account_Id.Text = value;
            }
        }


        public string Pw { get; set; } = default!;
               
        public string StudentName
        {
            get
            {
                return Account_StudentName.Text;
            }
            set
            {
                Account_StudentName.Text = value;
            }
        }

    }
}
