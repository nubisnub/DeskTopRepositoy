using System.Collections.ObjectModel;

namespace WpfPredictScore.Models
{
    public interface IQuizAnswer_Repository
    {
        ObservableCollection<int> CalculateQuizScores(ObservableCollection<int> userAnswers, ObservableCollection<int> correctAnswers);
    }
}