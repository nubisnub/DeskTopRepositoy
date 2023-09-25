using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp20230825.Commands;
using WpfApp20230825.Commons;
using WpfApp20230825.Models;

namespace WpfApp20230825.ViewModels
{
    public class ViewModel : ViewModelPropertyChanged
    {
        private IStudentDBRepository _studentRepository;
        private ObservableCollection<StudentDB>? _student_Data;
        private ComboBoxItem _selectedComboBoxItem=default!;

        private string _Name = "";
        private string _Day_Of_The_Class = "";
        private string _ReviewRatio = "";
        private readonly string _accountId;


        private void Delete(object _)
        {
            if (_studentRepository.Delete(Name, Day_Of_The_Class))
            {
                Clear();
                DisplayListView();
            }
        }
        private void Insert(Object _)
        {
            if (IsValid_Date())
            {
                StudentDB studentDB = GetStudent();
                MessageBox.Show("Insert OR Update를 실행합니다");
                _studentRepository.InsertOrUpdate(studentDB);
                Clear();
                DisplayListView();
            }
            else MessageBox.Show("날짜 및 복습도 입력을 확인해주세요");

        }
        private StudentDB GetStudent()
        {
            StudentDB A = new StudentDB();
            A.Name = Name;
            DateTime.TryParseExact(Day_Of_The_Class, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result);
            A.Day_Of_The_Class = result;
            A.ReviewRatio = int.Parse(ReviewRatio);
            return A;
        }

        //relaycommand용
        private bool IsValid_Date(object _)
        {
            if (_accountId.Equals("admin"))
            {
                return int.TryParse(ReviewRatio, out int _) && DateTime.TryParseExact(Day_Of_The_Class, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime _);
            }
            return false;
        }

        //insert유효성검사용
        private bool IsValid_Date()
        {
            bool isvalid = int.TryParse(ReviewRatio, out int _) && DateTime.TryParseExact(Day_Of_The_Class, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime _);
            return isvalid;
        }

        //ListView item 클릭
        private void ListViewClick(StudentDB studentDB)
        {
            Name = studentDB.Name;
            Day_Of_The_Class = studentDB.Day_Of_The_Class.ToString("yyyy-MM-dd");
            ReviewRatio = studentDB.ReviewRatio.ToString();
        }


        /// <summary>
        /// PlotModel 기본설정(유효한 데이터 변경은 UpdatePlotModel() 조작할 것
        /// </summary>
        private void SetPlotModel(int term)
        {
            IEnumerable<StudentDB>? studentDBs = _student_Data;
            studentDBs = studentDBs?.OrderBy(x => x.Day_Of_The_Class).TakeLast(term);
            var groupedData = studentDBs?.GroupBy(student => student.Name);

            OxyPlotOption plotOption = new OxyPlotOption("수업 성취도");

            //X,Y 축 추가
            plotOption.SetAxis_X("수업일");
            plotOption.SetAxis_Y("복습도");

            plotOption.SetRegend();
            if (groupedData != null)
            {
                foreach (var group in groupedData)
                {
                    LineSeries lineSeries = new LineSeries()
                    {
                        Title = group.Key,
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 3,
                    };

                    foreach (var student in group)
                    {
                        var date = student.Day_Of_The_Class;
                        var ratio = student.ReviewRatio;
                        lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), ratio));
                    }
                    plotOption.PlotModel.Series.Add(lineSeries);
                }
            }

            this.PlotModel = plotOption.PlotModel;
            OnPropertyChanged(nameof(PlotModel));

        }

        //유효데이터 변경시 조작할 것
        private void UpdatePlotModel()
        {
            if (SelectedItem != null)
            {
                string? selectedContent = SelectedItem.Content.ToString();
                if (selectedContent == "최근 일주일")
                {
                    // 최근 1주일(3개의 데이터)로 그래프를 설정합니다.
                    SetPlotModel(3);

                }
                else if (selectedContent == "최근 한 달")
                {
                    // 최근 한 달(8개의 데이터) 데이터로 그래프를 설정합니다.
                    SetPlotModel(8);
                }
                else if (selectedContent == "최근 세 달")
                {
                    // 최대 유효한 세 달(24개의 데이터)로 그래프를 설정합니다.
                    SetPlotModel(24);
                }
            }
        }
        public void Clear()
        {
            Name = "";
            Day_Of_The_Class = "";
            ReviewRatio = "";
        }


        public void DisplayListView()
        {
            Student_Data = _accountId.Equals("admin") ?
                 Student_Data = new ObservableCollection<StudentDB>(_studentRepository.GetAll()!)
               : Student_Data = new ObservableCollection<StudentDB>(_studentRepository.GetOne()!);
        }

        public ViewModel(IStudentDBRepository studentRepository, string accountId)
        {
            _studentRepository = studentRepository;
            _accountId = accountId;
            InsertOrUpdateCommand = new RelayCommand<object>(Insert, IsValid_Date);
            DeleteCommand = new RelayCommand<object>(Delete, IsValid_Date);
            NullCommand = new RelayCommand<object>(_ => Clear());
            ListViewClickCommand = new RelayCommand<StudentDB>(ListViewClick, null);
  

            DisplayListView();
        }

        public ComboBoxItem SelectedItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                _selectedComboBoxItem = value;
                UpdatePlotModel();
                OnPropertyChanged();
            }
        }


        public PlotModel? PlotModel { get; set; }

        public ICommand InsertOrUpdateCommand { get; set; }
        public ICommand NullCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ListViewClickCommand { get; set; }
        public ObservableCollection<StudentDB>? Student_Data
        {
            get => _student_Data;
            set
            {
                _student_Data = value;
                OnPropertyChanged();
            }

        }
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }
        public string Day_Of_The_Class
        {
            get => _Day_Of_The_Class;
            set
            {
                _Day_Of_The_Class = value;
                OnPropertyChanged();
            }
        }
        public string ReviewRatio
        {
            get => _ReviewRatio;
            set
            {
                _ReviewRatio = value;
                OnPropertyChanged();
            }
        }

    }
}