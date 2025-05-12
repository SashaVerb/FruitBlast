using System;
using UnityEngine;

public class TurnsController : MonoBehaviour
{
    [SerializeField] private TurnsView turnsView;
    [SerializeField] private int maxTurns;

    private int turnsLeft;

    private int TurnsLeft
    {
        get => turnsLeft;
        set
        {
            turnsLeft = value;
            turnsView.SetTurns(turnsLeft);
        }
    }

    private void Awake()
    {
        TurnsLeft = maxTurns;
    }

    public void MinusOneTurn()
    {
        if (TurnsLeft <= 0)
            return;
        
        --TurnsLeft;
        if (TurnsLeft <= 0)
        {
            Events.OnGameOver.Invoke();
        }
    }

    public bool CanMakeTurn()
    {
        return turnsLeft > 0;
    }
}
