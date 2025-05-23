using System.Collections;
using TMPro;
using UnityEngine;

public class PerksView : MonoBehaviour
{
    [SerializeField] private PerkSystem perkSystem;
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private PerkSlotView[] slots;
    [SerializeField] private GameObject wholeView;
    [SerializeField] private PerksViewEffect effect;
    
    public bool IsShowing => wholeView.activeSelf;

    private void Awake()
    {
        foreach (var slot in slots)
        {
            slot.OnClick.AddListener(HideView);
        }
        wholeView.SetActive(false);
    }

    public void ShowView(int level)
    {
        wholeView.SetActive(true);
        levelLabel.text = level.ToString();
        var (leftPerk, rightPerk) = perkSystem.GetTwoPerks();
        slots[0].Init(leftPerk);
        slots[1].Init(rightPerk);

        StartCoroutine(effect.ShowEffect());
    }

    public void HideView()
    {
        StartCoroutine(HidingRoutine());
    }

    private IEnumerator HidingRoutine()
    {
        yield return effect.HideEffect();
        wholeView.SetActive(false);
    }
}
