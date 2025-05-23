using System.Collections.Generic;

public class CircleBomb : Bomb
{
    public float Rad {get; set;}
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {
        ballsToDestroy = new () {this};
        ballsToDestroy.AddRange(GetBallsAround(Rad));
        return true;
    }
}
