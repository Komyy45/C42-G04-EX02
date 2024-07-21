using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject.UserInteractionServices
{
    // Give You the ability Read or Write Line to the Console Colored
    static class UserInteractionService
    {
        #region Methods

        public static string TakeInput(ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            string input = Console.ReadLine()?.ToLower().Trim() ?? string.Empty;
            Console.ForegroundColor = ConsoleColor.White;

            return input;
        }
        public static void ShowMessageLine<T>(T Message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void ShowMessage<T>(T Message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        } 

        #endregion
    }
}
