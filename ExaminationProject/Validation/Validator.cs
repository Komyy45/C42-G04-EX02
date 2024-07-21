using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationProject.Questions;
using ExaminationProject.UserInteractionServices;

namespace ExaminationProject.Validation
{
    // Helps you to validate user input
    static class Validator
    {
        #region Methods

        public static bool IsNotNumbersOnly(string expression)
        {
            if (expression is not null)
                foreach (var character in expression)
                    if (char.IsLetter(character)) return true;

            return false;
        }
        public static bool IsInstanceRepeated<T>(T[] arr, int index) where T : class
        {
            int length = arr.Length;
            for (int j = 0; j < index; j++)
            {
                if (arr[j]?.Equals(arr[index]) ?? false)
                    return true;
            }
            return false;
        }
        public static bool IsExcedingBoundary<T>(T[] arr, int option)
        {
            return option > arr.Length;
        } 

        #endregion
    }
}
