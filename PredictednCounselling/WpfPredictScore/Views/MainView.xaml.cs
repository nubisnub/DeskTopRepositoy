using System.Windows;

namespace WpfPredictScore.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        { 
            InitializeComponent();            
        }

        private void Exit_Cliked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
