using UnityEngine;

public class DieHard : Perk
{
    [SerializeField] private float startChance;
    [SerializeField] private float increaseChance;
    [Header("View")]
    [SerializeField] private string effectText;
    [SerializeField] private TextWithFadeEffect view;
    
    private Parameters parameters;
    
    private void Awake()
    {
        Init<DieHard>(this);
        parameters = new Parameters(startChance);
    }

    protected override void OnUpgrade()
    {
        parameters.Chance = startChance + increaseChance * (level - 1);
    }

    protected override bool TryTrigger()
    {
        if (Random.value < parameters.Chance)
        {
            view.ShowText(effectText);
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

        public Parameters(float chance)
        {
            this.Chance = chance;
        }
    }
}
