using System;
using UnityEngine;

public class Perk : MonoBehaviour
{
    [SerializeField] protected PerkSystem perkSystem;
    [SerializeField] protected string name;
    [SerializeField] protected Sprite icon;
    protected int level = 0;
    private bool isActive = false;
    
    public bool IsActive => isActive;
    public Sprite Icon => icon;
    public string Name => name;
    public int Level => level;
    public Type perkType {get; private set;}
    protected void Init<T>(Perk perk) where T : Perk
    {
        perkType = typeof(T);
        perkSystem.AddPerk(perk as T);
    }
    
    public void Upgrade()
    {
        isActive = true;
        level++;
        OnUpgrade();
    }

    public bool Triggered()
    {
        return isActive && TryTrigger();
    }

    protected virtual bool TryTrigger()
    {
        return true;
    }

    protected virtual void OnUpgrade() {}
}
