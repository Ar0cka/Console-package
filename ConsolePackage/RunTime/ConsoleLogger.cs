using System;

namespace ConsoleApp
{
    public static class ConsoleLogger
    {
        private static ILogs Logs { get; set; }

        public static void InitializeLogs(ILogs logs)
        {
            Logs = logs;
        }
        
        public static Action<string> LogAction { get; set; }
        
        public static void Info(string message) => Logs.Log(message);
        public static void Warning(string message) => Logs.Log(message);
        public static void Error(string message) => Logs.Log(message);
        public static void Command(string message) => Logs.Log(message);
    }
}