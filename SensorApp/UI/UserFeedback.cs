using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SensorApp.UI
{
    public static class UserFeedback
    {
        public static void ShowError(string message)
        {
            //Change after GUI implementation
            Console.WriteLine($"ERROR: {message}");
        }

        public static void ShowInfo(string message)
        {
            //Change after GUI implementation
            Console.WriteLine($"INFO: {message}");
        }
    }
}