using UnityEngine;

[CreateAssetMenu(fileName = "BallPhysicParameters", menuName = "Scriptable Objects/Balls/BallPhysicParameters")]
public class BallPhysicParameters : ScriptableObject
{
    public float minMass, maxMass;
    public float minRadius, maxRadius;
}
