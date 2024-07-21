using System;
using ExaminationProject.Validation;
using ExaminationProject.Answers;
using ExaminationProject.Questions;
using System.Diagnostics;
using ExaminationProject.UserInteractionServices;

namespace ExaminationProject.Exams
{
    sealed class PracticalExam : Exam
    {
        #region Constructors

        public PracticalExam(uint Minutes, ushort NumberOfQuestions) : base(Minutes, NumberOfQuestions)
        {
        }

        #endregion

        #region Methods

        /// Create the Questions of the PracticalExam
        public override void CreateQuestions()
        {
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                Console.Clear();
                string Header = GetQuestionHeader();

                string Body = GetQuestionBody();

                decimal Mark = GetQuestionMark();

                McqQuestion question = new McqQuestion(Header, Body, Mark);

                question.CreateAnswers();
                questions[i] = question;

                if (Validator.IsInstanceRepeated(questions, i))
                {
                    UserInteractionService.ShowMessageLine("This Question has been Added Already in this Exam!", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    i--;
                }
            }
        }

        /// Start a Practical Exam then after you finished answering Displays the Correct Answers
        public override void ShowExam()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var question in questions)
            {
                Console.Clear();
                UserInteractionService.ShowMessageLine(question, ConsoleColor.DarkYellow);

                uint answer;
                do
                {
                    UserInteractionService.ShowMessage("Please, Choose the Correct Answer: " , ConsoleColor.Green);
                }
                while (!uint.TryParse(UserInteractionService.TakeInput(), out answer) || answer == 0 || answer > question.NumberOfAnswers );

                if (new TimeOnly(stopwatch.ElapsedTicks) >= ExamTime)
                {
                    UserInteractionService.ShowMessageLine("\n----------------------------\nYou have Runned out of time!\n----------------------------\n", ConsoleColor.Red);
                    ShowModelAnswer();
                    return;
                }

            }

            stopwatch.Stop();

            Console.Clear();
            ShowModelAnswer();

            DisplayEndMessage(stopwatch);
        }

        /// Shows the Correct Answer of Every Question in the exam
        public override void ShowModelAnswer()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                UserInteractionService.ShowMessageLine($"({i + 1}) {questions[i][questions[i].CorrectAnswer - 1]}");
            }
        }

        #endregion
    }
}
