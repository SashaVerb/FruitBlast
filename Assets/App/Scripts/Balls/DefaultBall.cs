using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DefaultBall : Ball
{
    [SerializeField] private DefaultBallParameters parameters;
    [SerializeField] private DefaultBallPopEffect popEffect;
    
    public int Id { get; set; }

    private void Awake()
    {
        RandomizeParameters();
    }

    private void RandomizeParameters()
    {
        Id = Random.Range(0, parameters.sprites.Length);
        
        var ballVisuals = parameters.sprites[Id];
        insidePartSpriteRenderer.sprite = ballVisuals.fruitSprite;
    }
    
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {
        HashSet<Ball> markedToDestroy = new();
        Queue<Ball> toCheckNext = new();
        ballsToDestroy = new();
        List<Ball> otherBalls = new();
        
        markedToDestroy.Add(this);
        toCheckNext.Enqueue(this);
        ballsToDestroy.Add(this);

        while(toCheckNext.TryDequeue(out Ball ball))
        {
            foreach (var neighbour in ball.GetBallsAround())
            {
                if (neighbour is DefaultBall defaultBall)
                {
                    if (defaultBall.Id == Id && !markedToDestroy.Contains(neighbour))
                    {
                        markedToDestroy.Add(neighbour);
                        toCheckNext.Enqueue(neighbour);
                        ballsToDestroy.Add(neighbour);
                    }
                }
                else
                {
                    otherBalls.Add(neighbour);
                }
            }
        }

        if (ballsToDestroy.Count >= parameters.minNeighboursToPop)
        {
            ballsToDestroy.AddRange(otherBalls);
            return true;
        }
        else
        {
            ballsToDestroy = null;
            return false;
        }
    }
    
    protected override IEnumerator DestroyEffect()
    {
        Events.OnBallDestroyed.Invoke();
        
        PhysicsController.MakeExplosion(transform.position, physicBody.Radius + parameters.explosionExtraRadius, parameters.explosionForce);
        insidePartSpriteRenderer.enabled = false;
        
        Coroutine bubbleEffect = StartCoroutine(bubble.PlayEffect());
        Coroutine explosionEffect = StartCoroutine(popEffect.ExplosionEffect(insidePartSpriteRenderer.sprite));
        
        yield return bubbleEffect;
        yield return explosionEffect;
    }
}
