using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BallDestroyController : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyModificators;
    [SerializeField] private float destroyDelayForUnit = 0.1f;
    
    private List<IBallDestroyModifactor> modificators = new();

    private void Awake()
    {
        foreach (var potentialModificator in destroyModificators)
        {
            if (potentialModificator.TryGetComponent(out IBallDestroyModifactor modifactor))
            {
                modificators.Add(modifactor);
            }
        }
    }
    
    public void DestroyBalls(List<Ball> balls)
    {
        var firstBall = balls[0];
        Vector3 firstBallPos = firstBall.transform.position;
        
        var explosiveCharacter = PerkSystem.GetPerk<ExplosiveCharacter>();
        if (explosiveCharacter != null && balls.Count >= explosiveCharacter.MinBallsToTrigger && balls[0] is DefaultBall)
        {
            modificators.Add(explosiveCharacter);
        }
        
        var horizontalBombPerk = PerkSystem.GetPerk<HorizontalBombPerk>();
        if (horizontalBombPerk != null && balls.Count >= horizontalBombPerk.MinBallsToTrigger && balls[0] is DefaultBall)
        {
            modificators.Add(horizontalBombPerk);
        }
        
        foreach (var ball in balls)
        {
            if(ball.IsDestroyed)
                continue;
            
            ball.IsDestroyed = true;
            if (ball is DefaultBall)
            {
                foreach (var mod in modificators)
                {
                    mod.Modificate(ball);
                }
            }
            else if (ball is Bomb || ball is HorizontalBomb)
            {
                if (ball.TryPop(out List<Ball> ballsToExplode))
                {
                    DestroyBalls(ballsToExplode);
                }
            }
            
            ball.physicBody.IsStatic = true;
            ball.Destroy(Vector3.Distance(firstBallPos, ball.transform.position) * destroyDelayForUnit);
        }
        
        modificators.Remove(explosiveCharacter);
        modificators.Remove(horizontalBombPerk);
    }
}
