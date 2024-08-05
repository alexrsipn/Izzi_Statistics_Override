using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Izzi_Statistics_Override_WPF.Controller
{
    public class LoggerController
    {
        public enum LoggerOption
        {
            LOG = 0,
            OK = 1,
            ERROR = 2,
            DELETE = 3,
            PATCH = 4
        }
        private string _logPath;
        public string logPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }
        public void Logger(string line, LoggerOption loggerOption = LoggerOption.LOG) {
            string path = string.Empty;
            switch ((int)loggerOption)
            {
                case 1:
                    path = _logPath + "\\log_ok.txt";
                    break;
                case 2:
                    path = _logPath + "\\log_error.txt";
                    break;
                case 3:
                    path = _logPath + "\\log_delete.txt";
                    break;
                case 4:
                    path = _logPath + "\\log_patch.txt";
                    break;
                default:
                    path = logPath + "\\log.txt";
                    break;
            }
            try
            {
                StreamWriter file = new StreamWriter(path, true);
                file.WriteLine($"{DateTime.Now} : {line}");
                file.Close();
            }
            catch
            {
                Thread.Sleep(800);
                Logger(line, loggerOption);
            }
        }
    }
}
