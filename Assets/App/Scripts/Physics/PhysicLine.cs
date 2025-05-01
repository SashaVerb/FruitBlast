using UnityEngine;

public class PhysicLine : PhysicBody
{
    public Vector2 Normal => new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
}
