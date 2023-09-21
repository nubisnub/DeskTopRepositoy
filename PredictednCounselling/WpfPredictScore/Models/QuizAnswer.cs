namespace WpfPredictScore.Models
{
    public class QuizAnswer
    {        
        public QuizAnswer(int quizNum,int seletedAnswer, int isCorrect) {
            QuizNum = quizNum;
            SelectedAnswer = seletedAnswer;
            IsCorrect = isCorrect;
        }
        public int QuizNum { get; set; } = default!;
        public int SelectedAnswer { get; set; } = default!;
        public int IsCorrect {  get; set; }

    }
}
