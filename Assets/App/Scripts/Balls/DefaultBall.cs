using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBall : Ball
{
    public SpriteRenderer leftHalf, rightHalf;
    [SerializeField] private DefaultBallParameters defaultBallParameters;
    [SerializeField] private DefaultBallPopEffect popEffect;
    public override bool TryPop()
    {
        HashSet<Ball> ballsToDestroy = new();
        Queue<Ball> toCheckNext = new();
        List<BallDestroyInfo> toDestroy = new();

        ballsToDestroy.Add(this);
        toCheckNext.Enqueue(this);
        toDestroy.Add(new BallDestroyInfo(this, 0));

        while(toCheckNext.TryDequeue(out Ball ball))
        {
            BallDestroyInfo father = toDestroy.Find(info => info.ball == ball);
            foreach (var neighbour in ball.GetNeighbours())
            {
                if (neighbour.Id == Id && !ballsToDestroy.Contains(neighbour))
                {
                    ballsToDestroy.Add(neighbour);
                    toCheckNext.Enqueue(neighbour);
                    toDestroy.Add(new BallDestroyInfo(neighbour, father.step + 1));
                }
            }
        }

        if (toDestroy.Count >= defaultBallParameters.minNeighboursToPop)
        {
            foreach (var info in toDestroy)
            {
                info.ball.physicBody.IsStatic = true;
                info.ball.Destroy(info.step * defaultBallParameters.explosionDelay);
            }
            return true;
        }
        else 
            return false;
    }
    
    protected override IEnumerator DestroyRoutine(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        Events.OnBallDestroyed.Invoke();
        
        bubble.SetActive(false);
        PhysicsController.RemoveBody(physicBody);
        PhysicsController.MakeExplosion(transform.position, physicBody.Radius + ballParameters.extraRadiusForExplosion, ballParameters.explosionForce);
        fruitSpriteRenderer.enabled = false;
        
        yield return popEffect.ExplosionEffect();
        
        yield return base.DestroyRoutine();
    }
    
    private struct BallDestroyInfo
    {
        public Ball ball { get; set; }
        public int step { get; set; }

        public BallDestroyInfo(Ball ball, int step)
        {
            this.ball = ball;
            this.step = step;
        }
    }
}
