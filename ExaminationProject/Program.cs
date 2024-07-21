using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExaminationProject.Subjects;

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
                Console.Write("Do You Want to Start the exam [ Yes - No ]: ");
                aggrement = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
            }
            while (aggrement != "y" && aggrement != "n" && aggrement  != "yes" && aggrement != "no"); 

            if (aggrement.Contains('y'))
                subject?.Exam?.ShowExam();
            

            #endregion
        }
    }
}
