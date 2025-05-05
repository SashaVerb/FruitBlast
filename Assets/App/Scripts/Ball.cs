using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PhysicCircle physicBody;
    [Space]
    [SerializeField] private BallsParameters ballParameters;

    public int Id { get; set; }

    public virtual bool Pop() 
    {
        Debug.Log("Default pop reaction");
        return true;
    }

    public List<Ball> GetNeighbours()
    {
        var neighbours = PhysicsController.GetBodiesInArea(transform.position, physicBody.Radius + ballParameters.extraRadiusForDetection);

        List<Ball> result = new List<Ball>(neighbours.Count);
        foreach (var neighbour in neighbours)
        {
            result.Add(neighbour.GetComponent<Ball>());
        }

        return result;
    }

    private void OnDestroy()
    {
        Events.OnBallDestroyed.Invoke();
        PhysicsController.MakeExplosion(transform.position, physicBody.Radius + ballParameters.extraRadiusForExplosion, ballParameters.explosionForce);
    }
}
