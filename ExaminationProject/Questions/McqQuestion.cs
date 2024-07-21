using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.Validation;

namespace ExaminationProject.Questions
{
    class McqQuestion : Question , IMcqQuestion
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
                    Console.Write($"Please, Enter the Answer number {i + 1}: ");
                    answerText = Console.ReadLine()?.Trim() ?? string.Empty;

                }
                while (answerText == string.Empty);
                answers[i] = new Answer(answerText);

                // Checks if this answer exists 
                for (int j = 0; j < i; j++)
                    if (answers[i].Equals(answers[j]))
                    {
                        i--;
                        break;
                    }
            }

            // Take Answer from User
            byte correctAnswer;
            do
            {
                Console.Write("Please, Enter the number of the correct answer: ");
            } while (!byte.TryParse(Console.ReadLine(), out correctAnswer) || correctAnswer > answers.Length || correctAnswer == 0);

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
