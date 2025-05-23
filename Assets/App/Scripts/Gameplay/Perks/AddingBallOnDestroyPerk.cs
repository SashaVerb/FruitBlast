using UnityEngine;

public abstract class AddingBallOnDestroyPerk : Perk, IBallDestroyModifactor
{
    [SerializeField] protected Ball prefab;
    [SerializeField] protected BallFactory ballFactory;
    [SerializeField] private float startChance;
    [SerializeField] private float increaseChance;

    protected float Chance { get; private set; }

    protected void Awake()
    {
        Chance = startChance;
    }

    protected override void OnUpgrade()
    {
        Chance = startChance + increaseChance * (level - 1);
    }

    protected override bool TryTrigger()
    {
        return Random.value <= Chance;
    }

    protected abstract Ball ConfigureBall(Ball ball);

    public void Modificate(Ball ball)
    {
        ball.onDestroy.AddListener(() =>
        {
            if (Triggered())
            {
                var modificatorBall = ballFactory.AddBallToField(prefab);
                modificatorBall = ConfigureBall(modificatorBall);
                modificatorBall.transform.position = ball.transform.position;
            }
        });
    }
}
