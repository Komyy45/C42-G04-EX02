using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Answers;

namespace ExaminationProject.Questions
{
    class TrueFalseQuestion : Question
    {
        #region Constructors

        public TrueFalseQuestion(string Header, string Body, decimal Mark) : base(Header, Body, Mark, new Answer[2])
        {
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Body} ?\n 1) True 2) False";
        } 

        #endregion
    }
}
