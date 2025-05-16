using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBomb : Ball
{
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {
        var perk = PerkSystem.GetPerk<HorizontalBombPerk>();
        ballsToDestroy = new () {this};
        ballsToDestroy.AddRange(GetBallsInBox(physicBody.Radius + perk.GetParameters().Radius, physicBody.Radius));
        return true;
    }

    protected override IEnumerator DestroyRoutine(float delay = 0)
    {
        PhysicsController.RemoveBody(physicBody);
        
        yield return new WaitForSeconds(delay);
        
        yield return base.DestroyRoutine();
    }
}
