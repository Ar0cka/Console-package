using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleApp.ConsoleAttribute;

namespace ConsoleApp.ConsoleAttribute
{
    public static class CommandRegister
    {
        private static Dictionary<string, MethodInfo> _commands = new();

        public static void Register(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
                                                       BindingFlags.NonPublic))
                {
                    var attr = method.GetCustomAttribute<ConsoleCommand>();

                    if (attr != null)
                    {
                        _commands[attr.Command] = method;
                        ConsoleLogger.Info($"Initialize method {method.Name} with attribute {attr.Command}");
                    }
                }
            }
        }

        public static bool Execute(string commandName, string[] args)
        {
            if (_commands.TryGetValue(commandName, out var method))
            {
                var parameters = method.GetParameters();

                if (args.Length < parameters.Length)
                {
                    ConsoleLogger.Error($"Not enough arguments for command {{commandName}}");
                    return false;
                }

                object[] parserArgs = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    parserArgs[i] = ConvertArg(args[i], parameters[i].ParameterType);
                }

                object target = null;
                if (!method.IsStatic)
                {
                    var type = method.DeclaringType;
                    var instance = UnityEngine.Object.FindObjectOfType(type);
                    target = instance;
                }

                method.Invoke(target, parserArgs);
                return true;
            }

            ConsoleLogger.Error($"Command {commandName} not found");
            return false;
        }

        private static object ConvertArg(string arg, Type targetType)
        {
            if (targetType == typeof(string)) return arg;
            if (targetType == typeof(int)) return int.Parse(arg);
            if (targetType == typeof(bool)) return bool.Parse(arg);
            if (targetType == typeof(float)) return float.Parse(arg);

            return null;
        }
    }
}