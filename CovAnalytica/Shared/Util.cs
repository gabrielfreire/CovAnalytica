using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared
{
    public class Util
    {
        public static void LogInformation(string message)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString("dd/MM/yyyy H:mm:ss")} - [Information][Message '{message}']");
        }
        public static void LogException(Exception exception)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString("dd/MM/yyyy H:mm:ss")} - [Exception][Message '{exception.Message}']\nException:\n{exception.ToString()}\n\n");
        }
    }
}
