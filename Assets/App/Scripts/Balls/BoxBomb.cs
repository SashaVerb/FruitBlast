using System.Collections.Generic;
using UnityEngine;

public class BoxBomb : Bomb
{
    [SerializeField] private bool useRadiusForWidth, useRadiusForHeight;
    public float Width {get; set;}
    public float Height {get; set;}
    
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {
        ballsToDestroy = new () {this};
        ballsToDestroy.AddRange(GetBallsInBox(
            useRadiusForWidth ? physicBody.Radius : Width,
            useRadiusForHeight ? physicBody.Radius : Height));
        return true;
    }
}
