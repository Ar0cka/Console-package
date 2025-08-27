using System;
using ConsoleApp.Runtime;
using ConsoleApp.Runtime.ConsoleAttribute;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ConsoleApp.Runtime
{
    public class ConsoleUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject logPrefab;
        [SerializeField] private Transform logContent;
        [SerializeField] private Button sendButton;

        public void Initialize()
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            
            ConsoleLogger.LogAction += CreateNewLog;
            inputField.onEndEdit.AddListener(OnEndEdit);
            
            sendButton.onClick.AddListener(() =>
            {
                OnEndEdit(inputField.text);
            });
        }

        private void CreateNewLog(string message)
        {
            var prefab = Instantiate(logPrefab, logContent);
            TextMeshProUGUI text = prefab.GetComponent<TextMeshProUGUI>();
            text.text = message;
        }

        private void OnEndEdit(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            
            string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string commandName = parts[0];
            string[] args = parts.Length > 1 ? parts[1..] : Array.Empty<string>();
            
            bool success = CommandRegister.Execute(commandName, args);

            if (success)
            {
                ConsoleLogger.Command(text);
            }
            
            inputField.text = "";
        }

        private void OnApplicationQuit()
        {
            ConsoleLogger.LogAction -= CreateNewLog;
            inputField.onEndEdit.RemoveListener(OnEndEdit);
        }
    }
}