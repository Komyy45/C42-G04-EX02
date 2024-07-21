using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject.Validation
{
    static class Validator
    {
        public static bool IsNotNumbersOnly(string expression)
        {
            if (expression is not null)
                foreach (var character in expression)
                    if (char.IsLetter(character)) return true;

            return false;
        }
    }
}
