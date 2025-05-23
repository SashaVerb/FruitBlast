using UnityEngine;

public class TurnsController : MonoBehaviour
{
    [SerializeField] private ViewWithScaleEffect turnsView;
    [SerializeField] private int maxTurns;

    private int turnsLeft;

    private int TurnsLeft
    {
        get => turnsLeft;
        set
        {
            turnsLeft = value;
            turnsView.SetText(turnsLeft.ToString());
        }
    }

    private void Awake()
    {
        TurnsLeft = maxTurns;
    }

    public void MinusOneTurn()
    {
        var quickFinger = PerkSystem.GetPerk<QuickFinger>();
        if(quickFinger != null && quickFinger.IsActive)
            return;
        
        var dieHard = PerkSystem.GetPerk<DieHard>();
        if(dieHard != null && dieHard.Triggered())
            return;
        
        --TurnsLeft;
        if (TurnsLeft == 0)
        {
            Events.OnGameOver.Invoke();
        }
    }

    public bool CanMakeTurn()
    {
        return turnsLeft > 0;
    }
}
