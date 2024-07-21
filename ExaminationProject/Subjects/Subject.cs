using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;
using ExaminationProject.Exams;
using ExaminationProject.Questions;
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

        /// Creates PracticalExam or FinalExam
        public void CreateExam()
        {
            string Name;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Please, Enter the name of the Subject: ");
                Console.ForegroundColor = ConsoleColor.White;

                Name = Console.ReadLine() ?? string.Empty;
            } while (Name == string.Empty ||  !Validator.IsNotNumbersOnly(Name));

            ushort NumberOfQuestions;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Please, Enter Number of Questions: ");
                Console.ForegroundColor = ConsoleColor.White;

            } while (!ushort.TryParse(Console.ReadLine(), out NumberOfQuestions) || NumberOfQuestions == 0);

            uint Duration;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Please, Enter the Duration of the Exam in minutes: ");
                Console.ForegroundColor = ConsoleColor.White;

            } while (!uint.TryParse(Console.ReadLine(), out Duration) || Duration == 0);




            ExamType examType;
            string enteredType;
            do
            {
                Console.WriteLine("Please, Enter the of the Exam: \n1) Practical Exam \n2) Final Exam");
                enteredType = Console.ReadLine() ?? string.Empty;

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
                    Console.WriteLine("Invalid Exam Type!");
                    return;
            }
            ExamType = examType;
            Exam = exam;
        } 

        #endregion
    }
}
