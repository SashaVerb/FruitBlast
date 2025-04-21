using UnityEngine;

public class PhysicCircle : PhysicBody
{
    [SerializeField] private float radius = 1f;

    public float Radius { 
        get => radius;
        set
        {
            radius = value;
            CalculateRadiusChange();
        }
    }

    private void Awake()
    {
        CalculateRadiusChange();
    }

    private void OnValidate()
    {
        CalculateRadiusChange();
    }

    private void CalculateRadiusChange()
    {
        float scaleWithoutLocal = 1f;

        if (transform.lossyScale.x != 0)
        {
            scaleWithoutLocal = transform.localScale.x / transform.lossyScale.x;
        }

        transform.localScale = Vector3.one * 2f * radius * scaleWithoutLocal;
    }
}
