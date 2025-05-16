using UnityEngine;

public class HorizontalBombPerk : ExplosiveCharacter, IBallDestroyModifactor
{
    private void Awake()
    {
        Init<HorizontalBombPerk>(this);
        parameters = new Parameters(startChance, startRadius);
    }

    public new void Modificate(Ball ball)
    {
        ball.onDestroy.AddListener(() =>
        {
            if (Triggered())
            {
                var bomb = ballFactory.CreateHorizontalBall(parameters.Radius);
                bomb.transform.position = ball.transform.position;
            }
        });
    }
}
