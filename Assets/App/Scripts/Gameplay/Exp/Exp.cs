using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Exp : MonoBehaviour
{
    [SerializeField] private ExpCrystalInfo[] crystals;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MovingToTargetInArc movingToTargetInArc;
    [SerializeField] private DestroyWithDownScaleEffect destroyEffect;
    
    public UnityEvent OnDestroyMoment => destroyEffect.OnDestroyMoment;
    public int Cost { get; private set; }
    private void Awake()
    {
        float choice = Random.value, sum = 0f;
        
        foreach (var crystal in crystals)
        {
            sum += crystal.chance;
            if (choice <= sum)
            {
                spriteRenderer.sprite = crystal.icon;
                Cost = crystal.cost;
                break;
            }
        }
        
        movingToTargetInArc.onRichTarget.AddListener(destroyEffect.Destroy);
    }

    public void SetTarget(Vector3 position)
    {
        movingToTargetInArc.SetTarget(position);
    }

    [Serializable]
    private class ExpCrystalInfo
    {
        public Sprite icon;
        public int cost;
        public float chance;

        public ExpCrystalInfo(Sprite icon, int cost, float chance)
        {
            this.icon = icon;
            this.cost = cost;
            this.chance = chance;
        }
    }
}
