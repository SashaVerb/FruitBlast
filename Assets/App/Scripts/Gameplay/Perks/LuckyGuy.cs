using UnityEngine;

public class LuckyGuy : Perk
{
    [SerializeField] private float startChance;
    [SerializeField] private float increaseChance;
    [SerializeField] private float startMultiplier;
    [SerializeField] private float increaseMultiplier;
    [SerializeField] private float startCooldown;
    [SerializeField] private float decreaseCooldown;
    [Header("View")]
    [SerializeField] private string effectText;
    [SerializeField] private TextWithFadeEffect view;
    
    private Parameters parameters;
    private float lastTriggerTime = -1f;
    
    public float Multiplier => parameters.Multiplier;
    private void Awake()
    {
        Init<LuckyGuy>(this);
        parameters = new Parameters(startChance, startMultiplier, startCooldown);
    }

    protected override void OnUpgrade()
    {
        parameters.Chance = startChance + increaseChance * (level - 1);
        parameters.Multiplier = startMultiplier +  increaseMultiplier * (level - 1);
        parameters.Cooldown = startCooldown - decreaseCooldown * (level - 1);
    }

    public override bool Triggered()
    {
        if ((lastTriggerTime + parameters.Cooldown) <= Time.time && Random.value < parameters.Chance)
        {
            view.ShowText(effectText);
            lastTriggerTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public class Parameters
    {
        public float Chance {get; internal set;}
        public float Multiplier {get; internal set;}
        public float Cooldown {get; internal set;}

        public Parameters(float chance, float multiplier, float cooldown)
        {
            this.Chance = chance;
            this.Multiplier = multiplier;
            this.Cooldown = cooldown;
        }
    }
}
