// Class provides functionality to write logs

using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class Logs
    {
        public static void writeLog(string log)
        {
            string logDirectory = AppDomain.CurrentDomain.BaseDirectory + "logs.txt";
            IOFileManager.writeLogToFile(logDirectory, log);
        }
        
    }
}
