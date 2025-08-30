using System;
using System.IO;
using ConsoleApp.Runtime.ConsoleAttribute;
using UnityEngine;

namespace ConsoleApp.Runtime
{
    [DefaultExecutionOrder(-500)]
    public class Initialize : MonoBehaviour
    {
        [SerializeField] private string path;
        [SerializeField] private ConsoleUI consoleUI;
        
        private void Awake()
        {
            path = !string.IsNullOrEmpty(path) ? path : CreateFileInDocuments();
            
            ConsoleLogger.InitializeLogs(new Logger(path));
            
            consoleUI.Initialize();
            
            CommandRegister.Register(typeof(Initialize).Assembly);
            
            ConsoleLogger.Info("Starting ConsoleApp");
        }

        private string CreateFileInDocuments()
        {
            string pathToDocuments = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string fullPath = Path.Combine(pathToDocuments, "Log.txt");
            
            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, "Файл создан автоматически");
            }
            
            File.WriteAllText(fullPath, "");
            
            return fullPath;
        }
    }
}