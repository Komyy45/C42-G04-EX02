using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Quic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.Questions;
using ExaminationProject.UserInteractionServices;
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
                    UserInteractionService.ShowMessageLine("Please, Enter the Type of Your Question: ");

                    UserInteractionService.ShowMessage("(1) MCQ\t(2) True or False \n ------> ", ConsoleColor.DarkYellow);

                    Type = UserInteractionService.TakeInput();

                    if (Type != string.Empty && char.IsLetter(Type[0]))
                        Type = string.Join("", Type.Split(' '));
                }
                while (!Enum.TryParse(Type, true, out questionType) || questionType == 0 || Validator.IsExcedingBoundary(Enum.GetNames<QuestionType>(), (int)questionType));

                string Header = GetQuestionHeader();

                string Body = GetQuestionBody();

                decimal Mark = GetQuestionMark();

                Total += Mark;

                // Creates a question according to the Type User had Chosen
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
                            UserInteractionService.ShowMessage("Please, Enter the number of the Correct answer (1) True (2) False : ", ConsoleColor.DarkYellow);
                        }
                        while (!uint.TryParse(UserInteractionService.TakeInput(ConsoleColor.Green), out correctAnswer) || correctAnswer == 0 || correctAnswer > 2);
                        questions[i].CorrectAnswer = correctAnswer;
                        break;
                    default:
                        UserInteractionService.ShowMessageLine("Not Valid Type of Question!", ConsoleColor.Red);
                    break;
                }

                // Checks if the Question exists before
                if (Validator.IsInstanceRepeated(questions, i))
                {
                    UserInteractionService.ShowMessageLine("This Question has been Added Already in this Exam!", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    i--;
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
                UserInteractionService.ShowMessageLine(question, ConsoleColor.DarkYellow);
                uint Answer;
                do
                {
                    UserInteractionService.ShowMessage("Choose the Correct Answer:  ", ConsoleColor.Green);
                } 
                while (!uint.TryParse(UserInteractionService.TakeInput(), out Answer) || Answer == 0 || Answer > question.NumberOfAnswers);

                if (new TimeOnly(stopwatch.ElapsedTicks) >= ExamTime)
                {
                    UserInteractionService.ShowMessage("\n----------------------------\nYou have Runned out of time!\n----------------------------\n", ConsoleColor.Red);
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

            DisplayEndMessage(stopwatch);
        }

        /// Shows Your Grade in the exam
        public void ShowGrade(decimal MyGrade)
        {
            UserInteractionService.ShowMessage("Your Grade: ");
            if (MyGrade < Total * 0.5m)
                UserInteractionService.ShowMessage(MyGrade, ConsoleColor.Red);
            else if(MyGrade < Total * 0.75m)
                UserInteractionService.ShowMessage(MyGrade, ConsoleColor.DarkYellow);
            else
                UserInteractionService.ShowMessage(MyGrade, ConsoleColor.Green);

            UserInteractionService.ShowMessageLine($"/{Total}");
        }

        /// Shows Every Question With it's Correct Answer Underneath
        public override void ShowModelAnswer()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                UserInteractionService.ShowMessageLine(questions[i]);
                UserInteractionService.ShowMessageLine($"Correct Ansewer: ({questions[i].CorrectAnswer}) {questions[i][questions[i].CorrectAnswer - 1]}\n", ConsoleColor.Green);
            }
        }

        #endregion
    }
}
