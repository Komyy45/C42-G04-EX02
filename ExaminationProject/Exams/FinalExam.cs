using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.Questions;
using ExaminationProject.Validation;

namespace ExaminationProject.Exams
{
    sealed class FinalExam : Exam, IShowGrade
    {
        #region Properties

        public decimal Total { get; set; }

        #endregion

        #region Constructors

        public FinalExam(uint Minutes, ushort NumberOfQuestions) : base(Minutes, NumberOfQuestions)
        {
        }

        #endregion

        #region Methods

        /// Create Exam Questions
        public override void CreateQuestions()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                Console.Clear();

                QuestionType questionType;
                string Type;
                do
                {
                    Console.WriteLine("Please, Enter the Type of Your Question: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("(1) MCQ \n(2) True or False");
                    Console.ForegroundColor = ConsoleColor.White;

                    Type = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (Type != string.Empty && char.IsLetter(Type[0]))
                        Type = string.Join("", Type.Split(' '));

                }
                while (!Enum.TryParse(Type, true, out questionType) || questionType == 0 || (byte)questionType > Enum.GetNames<QuestionType>().Length);

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
                Total += Mark;

                switch (questionType)
                {
                    case QuestionType.Mcq:
                        McqQuestion question = new McqQuestion(Header, Body, Mark);
                        question.CreateAnswers();
                        questions[i] = question;
                        break;
                    case QuestionType.TrueOrFalse:
                        questions[i] = new TrueFalseQuestion(Header, Body, Mark);
                        questions[i][0] = new Answer("True");
                        questions[i][1] = new Answer("False");
                        uint correctAnswer;
                        do
                        {
                            Console.Write("Please, Enter the number of the Correct answer (1) True (2) False : ");
                        }
                        while (!uint.TryParse(Console.ReadLine(), out correctAnswer) || correctAnswer == 0 || correctAnswer > 2);
                        questions[i].CorrectAnswer = correctAnswer;
                        break;
                    default:
                        Console.WriteLine("Not Valid Type of Question!");
                        break;
                }
            }
        }

        /// Start a Final Exam then after you finished answering Displays Your Grade & Correct Answers
        public override void ShowExam()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            decimal MyGrade = 0;

            foreach (var question in questions)
            {

                Console.Clear();
                Console.WriteLine(question);
                uint Answer;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Choose the Correct Answer:  ");
                    Console.ForegroundColor = ConsoleColor.White;
                } 
                while (!uint.TryParse(Console.ReadLine(), out Answer) || Answer == 0 || Answer > question.NumberOfAnswers);

                if (new TimeOnly(stopwatch.ElapsedTicks) >= ExamTime)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n----------------------------\n");
                    Console.WriteLine("You have Runned out of time!");
                    Console.WriteLine("\n----------------------------\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    ShowModelAnswer();
                    ShowGrade(MyGrade);
                    return;
                }
                if (question.CorrectAnswer == Answer) MyGrade += question.Mark;
            }

            stopwatch.Stop();
            Console.Clear();

            ShowModelAnswer();

            ShowGrade(MyGrade);

            Console.WriteLine($"Time Taken: {stopwatch.Elapsed}");
            Console.Write($"\n----------------- Good Job :) -----------------\n");
        }

        /// Shows Your Grade in the exam
        public void ShowGrade(decimal MyGrade)
        {
            Console.Write("Your Grade: ");
            if (MyGrade < Total * 0.5m)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(MyGrade);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if(MyGrade < Total * 0.75m)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(MyGrade);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(MyGrade);
                Console.ForegroundColor = ConsoleColor.White;
            }
                Console.WriteLine($"/{ Total}");
            }

        /// Shows Every Question With it's Correct Answer Underneath
        public override void ShowModelAnswer()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine(questions[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Correct Ansewer: ({questions[i].CorrectAnswer}) {questions[i][questions[i].CorrectAnswer - 1]}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        #endregion
    }
}
