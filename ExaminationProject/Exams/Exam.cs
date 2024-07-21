using System.Diagnostics;
using ExaminationProject.Questions;
using ExaminationProject.UserInteractionServices;
using ExaminationProject.Validation;

namespace ExaminationProject.Exams
{
    abstract class Exam
    {
        #region Attributes

        private protected Question[] questions;

        #endregion

        #region Properties

        public TimeOnly ExamTime { get; set; }
        public ushort NumberOfQuestions { get; set; } 
        public Question this[int index]
        {
            get { return questions[index]; }
            set { questions[index] = value; }
        }

        #endregion

        #region Constructors

        protected Exam(uint Minutes, ushort NumberOfQuestions)
        {
            ExamTime = new TimeOnly(Minutes * 60 * 10_000_000);
            this.NumberOfQuestions = NumberOfQuestions;
            questions = new Question[NumberOfQuestions];
        }

        #endregion

        #region  Methods

        private protected string GetQuestionHeader()
        {
            string Header;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the Header of the Question: ", ConsoleColor.Green);
                Header = UserInteractionService.TakeInput();
            }
            while (Header == string.Empty || !Validator.IsNotNumbersOnly(Header));

            return Header;
        }
        private protected string GetQuestionBody()
        {
            string Body;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the Body of the Question: ", ConsoleColor.Green);
                Body = UserInteractionService.TakeInput();
            }
            while (Body == string.Empty || !Validator.IsNotNumbersOnly(Body));

            return Body;
        }
        private protected decimal GetQuestionMark()
        {
            decimal Mark;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the Mark of the Question: ", ConsoleColor.Green);
            }
            while (!decimal.TryParse(UserInteractionService.TakeInput(), out Mark) || Mark <= 0);

            return Mark;
        }
        private protected void DisplayEndMessage(Stopwatch stopwatch)
        {
            UserInteractionService.ShowMessageLine($"\nTime Taken: {stopwatch.Elapsed}", ConsoleColor.Green);
            UserInteractionService.ShowMessageLine($"\n----------------- Good Job :) -----------------\n", ConsoleColor.Green);
        }
        public abstract void CreateQuestions();
        public abstract void ShowExam();
        public abstract void ShowModelAnswer();

        #endregion
    }
}
