using System.Collections.Generic;
using UnityEngine;

public class BallDestroyController : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyModificatorsForEachBall;
    [SerializeField] private GameObject[] destroyModificatorsForChain;
    [SerializeField] private float destroyDelayForUnit = 0.1f;
    
    private List<IBallDestroyModifactor> modificatorsForEachBall = new();
    private List<IBallDestroyModifactor> modificatorsForChain = new();
    
    private void Awake()
    {
        foreach (var potentialModificator in destroyModificatorsForEachBall)
        {
            if (potentialModificator.TryGetComponent(out IBallDestroyModifactor modifactor))
            {
                modificatorsForEachBall.Add(modifactor);
            }
        }
        
        foreach (var potentialModificator in destroyModificatorsForChain)
        {
            if (potentialModificator.TryGetComponent(out IBallDestroyModifactor modifactor))
            {
                modificatorsForChain.Add(modifactor);
            }
        }
    }
    
    public void DestroyBalls(List<Ball> balls)
    {
        var firstBall = balls[0];
        Vector3 firstBallPos = firstBall.transform.position;
        
        foreach (var ball in balls)
        {
            if(ball.IsDestroyed)
                continue;
            
            ball.physicBody.IsStatic = true;
            ball.Destroy(Vector3.Distance(firstBallPos, ball.transform.position) * destroyDelayForUnit);
            
            if (ball is DefaultBall)
            {
                foreach (var mod in modificatorsForEachBall)
                {
                    mod.Modificate(ball);
                }
            }
            else if (ball is Bomb || ball is BoxBomb)
            {
                if (ball.TryPop(out List<Ball> ballsToExplode))
                {
                    DestroyBalls(ballsToExplode);
                }
            }
        }

        if (firstBall is DefaultBall)
        {
            foreach (var mod in modificatorsForChain)
            {
                mod.Modificate(firstBall);
            }
        }
    }
}
