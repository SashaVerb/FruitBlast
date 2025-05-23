using UnityEngine;

public class CircleScaler : MonoBehaviour
{
    [SerializeField] private float scale; 

    public float Scale => scale;
    
    public void RescaleCircle(PhysicCircle circle)
    {
        circle.Radius *= transform.localScale.x / scale;
    }

    public void AdjustObjectScale(Transform objectTransform)
    {
        objectTransform.localScale *= transform.localScale.x / scale;
    }
    
    public void RescaleAllCircles(float newScale)
    {
        float factor = newScale / transform.localScale.x;
        transform.localScale = new Vector3(newScale, newScale, newScale);

        foreach (PhysicCircle circle in GetComponentsInChildren<PhysicCircle>())
        {
            circle.Radius *= factor;
        }
    }
}
