using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject.Exams
{
    interface IShowGrade
    {
        public decimal Total { get; set; }
        public void ShowGrade(decimal MyGrade);

    }
}
