using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Questions;

namespace ExaminationProject.Exams
{
    abstract class Exam
    {
        #region Attributes

        private protected Question[] questions;

        #endregion

        #region Properties

        public TimeOnly ExamTime { get; set; }
        public ushort NumberOfQuestions { get; set; } 
        public Question this[int index]
        {
            get { return questions[index]; }
            set { questions[index] = value; }
        }

        #endregion

        #region Constructors

        protected Exam(uint Minutes, ushort NumberOfQuestions)
        {
            ExamTime = new TimeOnly(Minutes * 60 * 10_000_000);
            this.NumberOfQuestions = NumberOfQuestions;
            questions = new Question[NumberOfQuestions];
        }

        #endregion

        #region  Methods

        public abstract void CreateQuestions();
        public abstract void ShowExam();
        public abstract void ShowModelAnswer();

        #endregion
    }
}
