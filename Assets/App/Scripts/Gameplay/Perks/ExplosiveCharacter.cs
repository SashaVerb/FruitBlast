using UnityEngine;

public class ExplosiveCharacter : Perk, IBallDestroyModifactor
{
    [SerializeField] protected BallFactory ballFactory;
    [SerializeField] protected int minBallsToTrigger;
    [SerializeField] protected float startChance;
    [SerializeField] protected float increaseChance;
    [SerializeField] protected float startRadius;
    [SerializeField] protected float increaseRadius;
    
    protected Parameters parameters;

    public int MinBallsToTrigger => minBallsToTrigger;
    
    private void Awake()
    {
        Init<ExplosiveCharacter>(this);
        parameters = new Parameters(startChance, startRadius);
    }

    protected override void OnUpgrade()
    {
        parameters.Chance = startChance + increaseChance * (level - 1);
        parameters.Radius = startRadius + increaseRadius * (level - 1);
    }

    public override bool Triggered()
    {
        return Random.value <= parameters.Chance;
    }
    
    public Parameters GetParameters()
    {
        return parameters;
    }
    
    public class Parameters
    {
        public float Chance {get; internal set;}
        public float Radius {get; internal set;}

        public Parameters(float chance, float radius)
        {
            this.Chance = chance;
            this.Radius = radius;
        }
    }

    public void Modificate(Ball ball)
    {
        ball.onDestroy.AddListener(() =>
        {
            if (Triggered())
            {
                var bomb = ballFactory.CreateBomb(parameters.Radius);
                bomb.transform.position = ball.transform.position;
            }
        });
    }
}
