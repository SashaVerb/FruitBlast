using TMPro;
using UnityEngine;

public class TurnsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnsLabel;

    public void SetTurns(int turns)
    {
        turnsLabel.text = turns.ToString();
    }
}
