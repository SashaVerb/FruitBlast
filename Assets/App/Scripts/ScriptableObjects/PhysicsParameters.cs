using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsParameters", menuName = "Scriptable Objects/Physics Parameters")]
public class PhysicsParameters : ScriptableObject
{
    public float gravity;
    public float correctionPercent;
    public float allowedPenetration;
    public float minDeltaVelocity;
    public float restitution;
}
