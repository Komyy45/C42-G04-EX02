using System;
using ExaminationProject.Validation;
using ExaminationProject.Answers;
using ExaminationProject.Questions;
using System.Diagnostics;

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
                string Header;
                do
                {
                    Console.Write("Please, Enter the Header of the Question: ");
                    Header = Console.ReadLine() ?? string.Empty;
                }
                while (Header == string.Empty || !Validator.IsNotNumbersOnly(Header));

                string Body;
                do
                {
                    Console.Write("Please, Enter the Body of the Question: ");
                    Body = Console.ReadLine() ?? string.Empty;
                }
                while (Body == string.Empty || !Validator.IsNotNumbersOnly(Body));

                decimal Mark;
                do
                {
                    Console.Write("Please, Enter the Mark of the Question: ");
                }
                while (!decimal.TryParse(Console.ReadLine(), out Mark) || Mark <= 0);

                McqQuestion question = new McqQuestion(Header, Body, Mark);

                question.CreateAnswers();

                questions[i] = question;
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
                Console.WriteLine(question);

                uint answer;
                do
                {
                    Console.Write("Please, Choose the Correct Answer: ");
                }
                while (!uint.TryParse(Console.ReadLine(), out answer) || answer == 0 || answer > question.NumberOfAnswers );

                if (new TimeOnly(stopwatch.ElapsedTicks) >= ExamTime)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n----------------------------\n");
                    Console.WriteLine("You have Runned out of time!");
                    Console.WriteLine("\n----------------------------\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    ShowModelAnswer();
                    return;
                }

            }

            stopwatch.Stop();

            Console.Clear();
            ShowModelAnswer();

            Console.WriteLine($"Time Taken: {stopwatch.Elapsed}");
            Console.WriteLine($"\n--------------- Good Job :) ---------------\n");
        }

        /// Shows the Correct Answer of Every Question in the exam
        public override void ShowModelAnswer()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine($"({i + 1}) {questions[i][questions[i].CorrectAnswer - 1]}");
            }
        }

        #endregion
    }
}
