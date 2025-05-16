using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkSlotView : MonoBehaviour
{
    [SerializeField] private PerkSystem perkSystem;
    [SerializeField] private new TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Image image;
    [SerializeField] private Button applyButton;
    
    Type perkType;
    
    public readonly UnityEvent OnClick = new UnityEvent();
    
    private void Awake()
    {
        applyButton.onClick.AddListener(() => perkSystem.UpgradePerk(perkType));
    }
    
    public void Init(Perk perk)
    {
        perkType = perk.perkType;
        
        name.text = perk.Name;
        level.text = "уровень " + perk.Level;
        image.sprite = perk.Icon;
    }
}
