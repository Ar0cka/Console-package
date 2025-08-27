using System;
using ConsoleApp.ConsoleAttribute;
using UnityEngine;

namespace ConsoleApp
{
    [DefaultExecutionOrder(-100)]
    public class Initialize : MonoBehaviour
    {
        [SerializeField] private string path;
        [SerializeField] private ConsoleUI consoleUI;
        
        private void Awake()
        {
            ConsoleLogger.InitializeLogs(new Logger(path));
            
            consoleUI.Initialize();
            
            CommandRegister.Register(typeof(Initialize).Assembly);
            
            ConsoleLogger.Info("Starting ConsoleApp");
        }
    }
}