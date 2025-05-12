using TMPro;
using UnityEngine;

public class ErrorMessageView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;
    
    void Awake() {
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type) {
        if (type == LogType.Error || type == LogType.Exception) {
            debugText.text += $"Ошибка: {logString}\n{stackTrace}\n";
        }
    }
}
