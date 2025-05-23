using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Ball
{
    [SerializeField] private BombParameters parameters;
    [SerializeField] protected ParticleSystem destroyParticle;
    
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {   
        ballsToDestroy = new () {this};
        return true;
    }

    protected override IEnumerator DestroyEffect()
    {
        PhysicsController.MakeExplosion(transform.position, physicBody.Radius + parameters.explosionExtraRadius, parameters.explosionForce);
        
        destroyParticle.Play();
        yield return bubble.PlayEffect();
        
        yield return new WaitWhile(() => destroyParticle.isPlaying);;
    }
}
