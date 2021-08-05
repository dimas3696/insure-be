using System;
using Insure.Exceptions.CRM;

namespace Insure.Exceptions.Helpers
{
    public static class ConsoleError
    {
        public static void Print(string errorMessage)
        {
            var error = $"Message - {errorMessage}!";
            ConsolePrinter(error);
        }
        
        public static void Print(int statusCode, string statusDescription)
        {
            var error = $"{statusCode} - {statusDescription}.";
            ConsolePrinter(error);
        }
        public static void Print(int statusCode, string statusDescription, string errorMessage)
        {
            var error = $"{statusCode} - {statusDescription}. Message - {errorMessage}!";
            ConsolePrinter(error);
        }

        private static void ConsolePrinter(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}