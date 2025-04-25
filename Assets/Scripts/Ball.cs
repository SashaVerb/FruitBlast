using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PhysicCircle physicBody;
    [Space]
    [SerializeField] private float extraRadiusForDetection;

    public int Id { get; set; }

    public virtual void Pop() 
    {
        Debug.Log("Default pop reaction");
    }

    public List<Ball> GetNeighbours()
    {
        var neighbours = PhysicsController.GetBodiesInArea(transform.position, physicBody.Radius + extraRadiusForDetection);

        List<Ball> result = new List<Ball>(neighbours.Count);
        foreach (var neighbour in neighbours)
        {
            result.Add(neighbour.GetComponent<Ball>());
        }

        return result;
    }
}
