using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.UserInteractionServices;
using ExaminationProject.Validation;

namespace ExaminationProject.Questions
{
    class McqQuestion : Question , IAnswerFactory
    {
        #region Constructors

        public McqQuestion(string Header, string Body, decimal Mark) : base(Header, Body, Mark, new Answer[4])
        {
        }

        #endregion

        #region Methods

        /// Create Answers for the MCQ Question
        public void CreateAnswers()
        {
            for (int i = 0; i < answers.Length; i++)
            {
                string answerText;
                do
                {
                    UserInteractionService.ShowMessage($"Please, Enter the Answer number {i + 1}: ", ConsoleColor.DarkYellow);
                    answerText = UserInteractionService.TakeInput();

                }
                while (answerText == string.Empty);
                answers[i] = new Answer(answerText);

                // Checks if this answer exists before in the Same Question
                if (Validator.IsInstanceRepeated(answers, i))
                {
                    UserInteractionService.ShowMessageLine("This Answer has been Added Already in this Question!", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    i--;
                }
            }

            // Take Answer from User
            byte correctAnswer;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the number of the correct answer: ");
            } while (!byte.TryParse(UserInteractionService.TakeInput(), out correctAnswer) || correctAnswer > answers.Length || correctAnswer == 0);

            CorrectAnswer = correctAnswer;
        }
        public override string ToString()
        {
            StringBuilder McqQuestion = new StringBuilder();

            McqQuestion.AppendLine($"{Header}: ");
            McqQuestion.AppendLine("----------------------------\n");

            McqQuestion.AppendLine($"{Body} ?\n");

            for (int i = 0; i < answers.Length; i++)
            {
                McqQuestion.AppendLine($"({i + 1}) {answers[i]}");
            }

            return McqQuestion.ToString();
        } 

        #endregion
    }
}
