using System;
using System.Collections.ObjectModel;

namespace WpfPredictScore.Models
{
    public class QuizAnswer_Repository : IQuizAnswer_Repository
    {
        //view에서 받아온 답 userAnswers 실제 답 correctAnswers
        public ObservableCollection<int> CalculateQuizScores(ObservableCollection<int> userAnswers, ObservableCollection<int> correctAnswers)
        {
            ObservableCollection<int> results = new ObservableCollection<int>();

            if (userAnswers.Count != correctAnswers.Count)
            {
                throw new ArgumentException("Answer lists must have the same length.");
            }

            for (int i = 0; i < userAnswers.Count; i++)
            {
                int userAnswer = userAnswers[i];
                int correctAnswer = correctAnswers[i];

                //정답이면 1 오답이면 0
                if (userAnswer == correctAnswer)
                {
                    results.Add(1);
                }
                else
                {
                    results.Add(0);
                }
            }

            return results;
        }
    }
}
