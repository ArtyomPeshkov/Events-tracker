using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogErr(string message);
    }
}
