using TMPro;
using UnityEngine;

public class StartCountView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;

    public void SetText(string text)
    {
        label.text = text;
    }
}
