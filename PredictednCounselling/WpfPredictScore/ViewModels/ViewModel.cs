using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfPredictScore.Commands;
using WpfPredictScore.Models;

namespace WpfPredictScore.ViewModels
{
    public class ViewModel : ViewModelINotifyPropertyChanged
    {
        private readonly IQuizAnswer_Repository qrr;

        private ObservableCollection<QuizAnswer>? _answers;

        private string _Q1Answer = "";
        private string _Q2Answer = "";
        private string _Q3Answer = "";
        private string _Q4Answer = "";
        private string _Q5Answer = "";
        private string _Q6Answer = "";
        private string _Q7Answer = "";
        private string _Q8Answer = "";
        private string _Q9Answer = "";
        private string _Q10Answer = "";




        public ViewModel(IQuizAnswer_Repository iquizanswer_repository)
        {
            this.qrr = iquizanswer_repository;
            counsellingCommand = new RelayCommand<object>(CounsellingExe, IsValidExecute);
            SaveCommand = new RelayCommand<object>(SaveAnswer, IsValid);
            PredictCommand = new RelayCommand<object>(PredictScore, IsValidExecute);

        }

        public void PredictScore(object parameter)
        {
            if (_answers != null)
            {

                var input = new MLModelScorePredict.ModelInput
                {
                    Col0 = _answers[0].IsCorrect,
                    Col1 = _answers[1].IsCorrect,
                    Col2 = _answers[2].IsCorrect,
                    Col3 = _answers[3].IsCorrect,
                    Col4 = _answers[4].IsCorrect,
                    Col5 = _answers[5].IsCorrect,
                    Col6 = _answers[6].IsCorrect,
                    Col7 = _answers[7].IsCorrect,
                    Col8 = _answers[8].IsCorrect,
                    Col9 = _answers[9].IsCorrect,
                };



                MLModelScorePredict.ModelOutput prediction = MLModelScorePredict.Predict(input);
                int score = (int)prediction.Score;
                MessageBox.Show(StudentName + "님의 " + "기말 예상 점수는 " + score.ToString() + " 입니다.");
            }

        }

        public void CounsellingExe(object _)
        {          
            var chatView = new CounsellingChatClient.MainWindow();
            chatView.Show();
        }

        public bool IsValid(object _)
        {
            for (int i = 0; i < 10; i++)
            {
                string query = $"Q{i + 1}Answer";
                object? propertyValue = typeof(ViewModel).GetProperty(query)?.GetValue(this);

                if (propertyValue == null || !int.TryParse(propertyValue.ToString(), out int result))
                {
                    return false;
                }

                //null 이슈
                //if (!int.TryParse((string)typeof(ViewModel).GetProperty(query)?.GetValue(this), out int result)) return false;
            }

            return true;
        }
        public bool IsValidExecute(object _)
        {
            if (_answers == null || _answers.Count != 10) return false;
            return true;
        }

        public void SaveAnswer(object _)
        {
            QuizRightAnswer quizRightAnswer = new QuizRightAnswer();
            int[] quizrightAnswer = quizRightAnswer.GetQuizRightAnswer;

            ObservableCollection<QuizAnswer> answer = new ObservableCollection<QuizAnswer>();
            for (int i = 0; i < quizrightAnswer.Length; i++)
            {

                string query = $"Q{i + 1}Answer";
                object? propertyValue = typeof(ViewModel).GetProperty(query)?.GetValue(this);

                if (propertyValue == null || !int.TryParse(propertyValue.ToString(), out int selectedAnswer))
                {
                    // 처리할 수 없는 경우 예외처리 또는 기본값으로 처리
                    selectedAnswer = 0; // 혹은 다른 기본값 사용
                }

                if (selectedAnswer == quizrightAnswer[i])
                {
                    answer.Add(new QuizAnswer(i + 1, selectedAnswer, 1));
                }
                else answer.Add(new QuizAnswer(i + 1, selectedAnswer, 0));




                //null이슈
                //string query = $"Q{i + 1}Answer";
                //int seletedAnswer = int.Parse((string)typeof(ViewModel).GetProperty(query)?.GetValue(this));
                //if (seletedAnswer == quizrightAnswer[i])
                //{
                //    answer.Add(new QuizAnswer(i + 1, seletedAnswer, 1));
                //}
                //else answer.Add(new QuizAnswer(i + 1, seletedAnswer, 0));




            }
            MessageBox.Show("저장완료");

            _answers = answer;
            SubmittedAnswer = _answers;

            OnPropertyChange();





        }




        /// <summary>
        /// View Property
        /// </summary>
        public string StudentName { get; set; } = string.Empty;
        public string Q1Answer
        {
            get => _Q1Answer; set
            {
                _Q1Answer = value;
                OnPropertyChange();
            }
        }
        public string Q2Answer
        {
            get => _Q2Answer; set
            {
                _Q2Answer = value;
                OnPropertyChange();
            }
        }
        public string Q3Answer
        {
            get => _Q3Answer; set
            {
                _Q3Answer = value;
                OnPropertyChange();
            }
        }
        public string Q4Answer
        {
            get => _Q4Answer; set
            {
                _Q4Answer = value;
                OnPropertyChange();
            }
        }
        public string Q5Answer
        {
            get => _Q5Answer; set
            {
                _Q5Answer = value;
                OnPropertyChange();
            }
        }
        public string Q6Answer
        {
            get => _Q6Answer; set
            {
                _Q6Answer = value;
                OnPropertyChange();
            }
        }
        public string Q7Answer
        {
            get => _Q7Answer; set
            {
                _Q7Answer = value;
                OnPropertyChange();
            }
        }
        public string Q8Answer
        {
            get => _Q8Answer; set
            {
                _Q8Answer = value;
                OnPropertyChange();
            }
        }
        public string Q9Answer
        {
            get => _Q9Answer; set
            {
                _Q9Answer = value;
                OnPropertyChange();
            }
        }
        public string Q10Answer
        {
            get => _Q10Answer; set
            {
                _Q10Answer = value;
                OnPropertyChange();
            }
        }
        public ObservableCollection<QuizAnswer>? SubmittedAnswer
        {
            get => _answers;
            set
            {
                _answers = value;
                OnPropertyChange();
            }
        }


        public ICommand counsellingCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand? PredictCommand { get; set; }


    }
}
