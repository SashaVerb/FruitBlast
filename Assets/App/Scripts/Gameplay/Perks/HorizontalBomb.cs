using UnityEngine;

public class HorizontalBomb : BoxBombPerk
{
    private void Awake()
    {
        base.Awake();
        Init<HorizontalBomb>(this);
    }
    protected override Ball ConfigureBall(Ball ball)
    {
        if (ball is BoxBomb circleBomb)
        {
            circleBomb.Width = parameters.Length;
        }

        return ball;
    }
}
