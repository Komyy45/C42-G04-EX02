using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;

namespace ExaminationProject.Questions
{
    abstract class Question
    {
        #region Attributes

        private protected Answer[] answers;

        #endregion

        #region Properties

        public string Header { get; set; }

        public string Body { get; set; }

        public decimal Mark { get; set; }

        public byte NumberOfAnswers {  get; set; }

        public Answer this[uint index]
        {
            get { return answers[index]; }
            set { answers[index] = value; }
        }

        public uint CorrectAnswer { get; set; }


        #endregion

        #region Constructors

        protected Question(string Header, string Body, decimal Mark, Answer[] answers)
        {
            this.Header = Header;
            this.Body = Body;
            this.Mark = Mark;
            this.NumberOfAnswers = (byte)answers.Length;
            this.answers = answers;
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return HashCode.Combine(Body);
        }

        public override bool Equals(object? obj)
        {
            if(obj is Question question)
                return Body.Equals(question?.Body);

            return false;
        }

        #endregion

    }
}
