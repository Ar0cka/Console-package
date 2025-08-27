using System;

namespace ConsoleApp.Runtime.ConsoleAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommand : Attribute
    {
        public string Command { get; }

        public ConsoleCommand(string command)
        {
            Command = command;
        }
    }
}