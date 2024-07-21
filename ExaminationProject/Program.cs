using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExaminationProject.Subjects;
using ExaminationProject.UserInteractionServices;

namespace ExaminationProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Create Exam

            Subject subject = new Subject();
            subject.CreateExam();

            #endregion

            #region Show Exam

            string aggrement;
            do
            {
                UserInteractionService.ShowMessage("\nDo You Want to Start the exam [ Yes - No ]: ", ConsoleColor.Green);
                aggrement = UserInteractionService.TakeInput();
            }
            while (aggrement != "y" && aggrement != "n" && aggrement  != "yes" && aggrement != "no"); 

            if (aggrement.Contains('y'))
                subject?.Exam?.ShowExam();
            
            #endregion
        }
    }
}
