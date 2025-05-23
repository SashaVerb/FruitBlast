using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ball : MonoBehaviour
{
    public SpriteRenderer insidePartSpriteRenderer;
    public PhysicCircle physicBody;
    public BubblePopEffect bubble;

    [HideInInspector] 
    public readonly UnityEvent onDestroy = new();
    
    public bool IsDestroyed { get; private set; } = false;

    public abstract bool TryPop(out List<Ball> ballsToDestroy);

    public List<Ball> GetBallsAround(float extraRadius = 0f)
    {
        var neighbours = PhysicsController.GetBodiesInArea(transform.position, physicBody.Radius + extraRadius);

        List<Ball> result = new(neighbours.Count);
        foreach (var neighbour in neighbours)
        {
            var ball = neighbour.GetComponent<Ball>();
            if (ball != null)
                result.Add(ball);
        }

        return result;
    }
    
    public List<Ball> GetBallsInBox(float width, float height)
    {
        var neighbours = PhysicsController.GetBodiesInBox(transform.position, width, height);

        List<Ball> result = new(neighbours.Count);
        foreach (var neighbour in neighbours)
        {
            var ball = neighbour.GetComponent<Ball>();
            if (ball != null)
                result.Add(ball);
        }

        return result;
    }
    
    public void Destroy(float delay = 0f)
    {
        IsDestroyed = true;
        StartCoroutine(DestroyRoutine(delay));
    }

    private IEnumerator DestroyRoutine(float delay)
    {
        PhysicsController.RemoveBody(physicBody);
        yield return new WaitForSeconds(delay);
        onDestroy.Invoke();
        yield return DestroyEffect();
        Destroy(gameObject);
    }
    protected virtual IEnumerator DestroyEffect()
    {
        yield return null;
    }
}
