using System;
using System.Data;
using System.IO;
using UnityEngine;

namespace ConsoleApp
{
    public class Logger : ILogs, IDisposable
    {
        private readonly StreamWriter _writer;

        public Logger(string filePath)
        {
            _writer = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                AutoFlush = true
            };
        }

        public void Log(string message) => Write("INFO", message);
        public void Warning(string message) => Write("WARN", message);
        public void Error(string message) => Write("ERROR", message);
        public void Command(string message) => Write("COMMAND", message);
        
        private void Write(string level, string message)
        {
            string log = $"[{level}] {message}";
            ConsoleLogger.LogAction?.Invoke(log);
            _writer.WriteLine(log);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }

    public interface ILogs
    {
        void Log(string message);
        void Warning(string message);
        void Error(string message);
        void Command(string message);
    }
}