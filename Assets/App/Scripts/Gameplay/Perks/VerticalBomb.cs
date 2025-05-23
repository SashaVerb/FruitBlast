using UnityEngine;

public class VerticalBomb : BoxBombPerk
{
    private void Awake()
    {
        base.Awake();
        Init<VerticalBomb>(this);
    }
    protected override Ball ConfigureBall(Ball ball)
    {
        if (ball is BoxBomb circleBomb)
        {
            circleBomb.Height = parameters.Length;
        }

        return ball;
    }
}
