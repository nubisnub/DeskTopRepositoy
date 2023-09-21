using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPredictScore.Models;
using WpfPredictScore.ViewModels;
using WpfPredictScore.Views;

namespace WpfPredictScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StarClick(object sender, RoutedEventArgs e)
        {
            var qrr = new QuizAnswer_Repository();
            var mv = new MainView()
            {
                DataContext = new ViewModel(qrr)
            };
            mv.Show();
        }
    }
}
