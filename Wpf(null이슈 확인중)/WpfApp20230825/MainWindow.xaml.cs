using System.Collections.Generic;
using System.Windows;
using WpfApp20230825.Models;
using WpfApp20230825.MVC_Account.NewJoinControllers;
using WpfApp20230825.MVC_Account.NewJoinModels;
using WpfApp20230825.ViewModels;
using WpfApp20230825.Views;


namespace WpfApp20230825
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MVC 와 MVVM 연습용으로 만든겁니다. 회원가입은 MVC 구조로 로그인
        //후 데이터조회는 MVVM으로 구성하였습니다.
        /// <summary>
        /// </summary>
        private AccountsRepository _accountsRepository;
        private AccountsController _accountsController;
        private Accounts? _accounts;
        private List<Accounts> accounts = new List<Accounts>();


        public MainWindow()
        {
            _accountsRepository = new AccountsRepository();
            InitializeComponent();
            _accountsController = new AccountsController(this, _accountsRepository);
        }

        private void Enterbtn_Clicked(object sender, RoutedEventArgs e)
        {
            _accountsRepository = new AccountsRepository();
            string loginPw = _accountsController.PasswordConvert(passwordBox.Password);

            _accounts = _accountsRepository.Check_Account(loginId.Text, loginPw);

            if (!_accounts.Id.Equals(""))
            {
                string greetingString = _accounts.Id.Equals("admin") ?
                    "안녕하세요 선생님 고생많으십니다." :
               "안녕하세요 " + $"{_accountsRepository.Check_Account(loginId.Text, loginPw).StudentName}학생" + " 학부모님 반갑습니다!";

                MessageBox.Show(greetingString);

                StudentDBRepository studentRepository = new StudentDBRepository();
                studentRepository.AccountInfo = _accounts;
                var mv = new MainView()
                {
                    DataContext = new ViewModel(studentRepository, _accounts.Id)
                };
                mv.Show();
            }
            else
            {
                MessageBox.Show("아이디와 비밀번호를 확인해주세요");
            }

        }

        private void NewJoin_Clicked(object sender, RoutedEventArgs e)
        {

            NewJoinPage NJP = new NewJoinPage();
            var accountsRepository = new AccountsRepository();
            _ = new AccountsController(NJP, accountsRepository);


            NJP.Show();
        }
    }
}
