using TMPro;
using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    private static string consoleLog = "";

    [Tooltip("The ConsoleGUI GameObject")]
    [SerializeField] private GameObject consoleGUI;

    private TextMeshProUGUI textArea;

    private void Start()
    {
        textArea = consoleGUI.GetComponentInChildren<TextMeshProUGUI>();
        if (textArea == null) throw new System.Exception("No GameObject containing a TextMeshProUGUI component on a child passed to ConsoleToGUI");
    }

    void OnEnable()
    {
        // Subscribe to Application.logMessageReceived and call Log when it occurs
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        // Unsubscribe from Application.logMessageReceived
        Application.logMessageReceived -= Log;
    }

    // Get the newest Console log and put it infront of the other text so far
    public void Log(string logString, string _, LogType logType)
    {
        string logTypeText = logType.ToString();
        // Give the log type a color
        if(logType == LogType.Error || logType == LogType.Exception) 
        {
            logTypeText = "<color=red>" + logTypeText + "</color>";
        } else if (logType == LogType.Warning)
        {
            logTypeText = "<color=yellow>" + logTypeText + "</color>";
        } else
        {
            logTypeText = "<color=grey>" + logTypeText + "</color>";
        }
        logTypeText += ": ";
        // Put it infornt of the other text so far
        consoleLog = logTypeText + logString + "\n" + consoleLog;
        textArea.text = consoleLog;
    }

    // Toggle the state of the console
    public void ToggleConsole()
    {
        consoleGUI.SetActive(!consoleGUI.activeSelf);
    }
}