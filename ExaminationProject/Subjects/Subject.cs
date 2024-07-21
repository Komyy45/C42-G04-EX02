using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.Exams;
using ExaminationProject.Questions;
using ExaminationProject.UserInteractionServices;
using ExaminationProject.Validation;

namespace ExaminationProject.Subjects
{
    class Subject
    {
        #region Attributes

        private uint id;

        #endregion

        #region Properties

        public uint Id { get { return id; } }
        public string? Name { get; set; }
        public Exam? Exam { get; set; }
        public ExamType ExamType { get; set; }
        public static uint NumberOfSubjectsCreated { get; private set; }

        #endregion

        #region Constructors

        static Subject()
        {
            NumberOfSubjectsCreated = 1;
        }
        public Subject()
        {
            id = NumberOfSubjectsCreated++;
        }

        #endregion

        #region Methods

        /// Creates an Exam instance according to user's Choise
        public void CreateExam()
        {
            string Name;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the name of the Subject: ", ConsoleColor.Green);
                Name = UserInteractionService.TakeInput();
            } while (Name == string.Empty ||  !Validator.IsNotNumbersOnly(Name));

            ushort NumberOfQuestions;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter Number of Questions: ", ConsoleColor.Green);
            } while (!ushort.TryParse(UserInteractionService.TakeInput(), out NumberOfQuestions) || NumberOfQuestions == 0);

            uint Duration;
            do
            {
                UserInteractionService.ShowMessage("Please, Enter the Duration of the Exam in minutes: ", ConsoleColor.Green);
            } while (!uint.TryParse(UserInteractionService.TakeInput(), out Duration) || Duration == 0);


            ExamType examType;
            string enteredType;
            do
            {
                UserInteractionService.ShowMessageLine("Please, Enter the of the Exam: \n1) Practical Exam \n2) Final Exam");
                enteredType = UserInteractionService.TakeInput();

                if (enteredType.Length > 0 && char.IsLetter(enteredType[0]))
                    enteredType = string.Join("", enteredType.Split(" "));

            } while (!Enum.TryParse(enteredType, true, out examType) || examType == 0 || (byte)examType > Enum.GetNames<ExamType>().Length);

            
            Exam exam;
            switch (examType)
            {
                case ExamType.PracticalExam:
                    exam = new PracticalExam(Duration, NumberOfQuestions);
                    exam.CreateQuestions();
                    break;
                case ExamType.FinalExam:
                    exam = new FinalExam(Duration, NumberOfQuestions);
                    exam.CreateQuestions();
                    break;
                default:
                    UserInteractionService.ShowMessageLine("Invalid Exam Type!" , ConsoleColor.Red);
                    return;
            }
            ExamType = examType;
            Exam = exam;
        } 

        #endregion
    }
}
