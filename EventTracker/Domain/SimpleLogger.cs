using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SimpleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine("Info: " + message);
        }
        public void LogWarn(string message)
        {
            Console.WriteLine("Warn: " + message);
        }
        public void LogErr(string message)
        {
            Console.WriteLine("Error: " + message);
        }
    }
}
