using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject.Answers
{
    class Answer
    {
        #region Attributes

        private uint id;

        #endregion

        #region Properties

        public uint Id { get { return id; } }
        public static uint NumberOfAnswers { get; private set; }
        public string AnswerText { get; set; }

        #endregion

        #region Constructors

        static Answer()
        {
            NumberOfAnswers = 1;
        }
        public Answer(string AnswerText)
        {
            id = NumberOfAnswers++;
            this.AnswerText = AnswerText;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return AnswerText;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AnswerText);
        }

        public override bool Equals(object? obj)
        {
            Answer? answer = obj as Answer;

            return AnswerText.Equals(answer?.AnswerText);
        }

        #endregion
    }

}
