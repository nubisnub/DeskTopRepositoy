using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf230823MVVM.Models;

namespace Wpf230823MVVM.ViewModels
{
    public class MainViewModel
    {
        private readonly IPeople_Repository _People_Repository;
        private string _Id;
        private string _Name;
        private string _Sex;
        private int _Age;
        

        public MainViewModel(People_Repository People_Repository)
        {
            _People_Repository = People_Repository;
        }

       


    }
}
