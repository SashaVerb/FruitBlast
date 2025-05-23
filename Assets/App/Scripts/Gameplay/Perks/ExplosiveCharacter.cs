using UnityEngine;

public class ExplosiveCharacter : AddingBallOnDestroyPerk
{
    [SerializeField] protected float startRadius;
    [SerializeField] protected float increaseRadius;
    
    protected Parameters parameters;
    
    private void Awake()
    {
        base.Awake();
        Init<ExplosiveCharacter>(this);
        parameters = new Parameters(Chance, startRadius);
    }

    protected override void OnUpgrade()
    {
        base.OnUpgrade();
        parameters.Radius = startRadius + increaseRadius * (level - 1);
    }

    protected override Ball ConfigureBall(Ball ball)
    {
        if (ball is CircleBomb circleBomb)
        {
            circleBomb.Rad = parameters.Radius;
        }

        return ball;
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
}
