using UnityEngine;

namespace ConsoleApp.Runtime
{
    public class InputScript : MonoBehaviour
    {
        [SerializeField] private GameObject consoleObject;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                consoleObject.SetActive(!consoleObject.activeSelf);
            }
        }
    }
}