using WpfApp20230825.MVC_Account.NewJoinControllers;

namespace WpfApp20230825.MVC_Account.NewjoinViews
{
    public interface INewJoinPage
    {
        string Id { get; set; }

        string StudentName { get; set; }
        string Pw { get; }

        void SetController(AccountsController controller);
    }
}