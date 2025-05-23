using UnityEngine;

public class BoxBombPerk : AddingBallOnDestroyPerk
{
    [SerializeField] protected float startLength;
    [SerializeField] protected float increaseLength;
    
    protected Parameters parameters;
    
    protected void Awake()
    {
        base.Awake();
        parameters = new Parameters(Chance, startLength);
    }

    protected override void OnUpgrade()
    {
        base.OnUpgrade();
        parameters.Length = startLength + increaseLength * (level - 1);
    }

    protected override Ball ConfigureBall(Ball ball)
    {
        if (ball is BoxBomb circleBomb)
        {
            circleBomb.Height = parameters.Length;
            circleBomb.Width = parameters.Length;
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
        public float Length {get; internal set;}

        public Parameters(float chance, float length)
        {
            this.Chance = chance;
            this.Length = length;
        }
    }
}
