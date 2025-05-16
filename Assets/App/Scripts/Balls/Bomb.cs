using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Ball
{
    [SerializeField] private BombParameters parameters;
    
    public float Radius { get; set; }
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {   
        ballsToDestroy = new () {this};
        ballsToDestroy.AddRange(GetBallsAround(Radius));
        return true;
    }

    protected override IEnumerator DestroyRoutine(float delay = 0)
    {
        PhysicsController.RemoveBody(physicBody);
        PhysicsController.MakeExplosion(transform.position, physicBody.Radius + parameters.explosionExtraRadius, parameters.explosionForce);
        
        yield return new WaitForSeconds(delay);
        
        yield return base.DestroyRoutine();
    }
}
