using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp20230825.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        private void TxtChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression be = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
        public MainView()
        {
            InitializeComponent();
            Txt_StudentName.TextChanged += TxtChanged;
            Txt_StudentDay_Of_The_Class.TextChanged += TxtChanged;
            Txt_StudentReviewRatio.TextChanged += TxtChanged;
        }
    }
}
