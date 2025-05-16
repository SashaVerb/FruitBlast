using UnityEngine;

[CreateAssetMenu(fileName = "BallsParameters", menuName = "Scriptable Objects/Balls Parameters")]
public class BallsParameters : ScriptableObject
{
    public float extraRadiusForDetection, explosionForce, extraRadiusForExplosion;
}
