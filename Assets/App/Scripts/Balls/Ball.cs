using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public SpriteRenderer fruitSpriteRenderer;
    public PhysicCircle physicBody;
    public GameObject bubble;
    [Space]
    [SerializeField] protected BallsParameters ballParameters;

    public int Id { get; set; }

    public virtual bool TryPop() 
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
    
    public void Destroy(float delay = 0f)
    {
        StartCoroutine(DestroyRoutine(delay));
    }

    protected virtual IEnumerator DestroyRoutine(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        
        Destroy(gameObject);
    }
}
