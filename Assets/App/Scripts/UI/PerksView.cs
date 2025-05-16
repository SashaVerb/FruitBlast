using TMPro;
using UnityEngine;

public class PerksView : MonoBehaviour
{
    [SerializeField] private PerkSystem perkSystem;
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private PerkSlotView[] slots;
    [SerializeField] private GameObject wholeView;
    
    public bool IsShowing => wholeView.activeSelf;

    private void Awake()
    {
        foreach (var slot in slots)
        {
            slot.OnClick.AddListener(() => wholeView.SetActive(false));
        }
    }

    public void ShowView(int level)
    {
        wholeView.SetActive(true);
        var (leftPerk, rightPerk) = perkSystem.GetTwoPerks();
    }
    
}
