namespace WpfPredictScore.Models
{
    public class QuizRightAnswer
    {
        //정답설정 단순하게 1번 1 2번 2 식으로 지정함.
        private readonly int[] _quizRightAnswer ={ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

        public int[] GetQuizRightAnswer
        {
            get
            {
                return _quizRightAnswer;
            }
               }
    }
}
