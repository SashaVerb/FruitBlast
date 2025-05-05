using UnityEngine;

[CreateAssetMenu(fileName = "BallsParameters", menuName = "Scriptable Objects/Balls Parameters")]
public class BallsParameters : ScriptableObject
{
    public float minMass, maxMass;
    public float minRadius, maxRadius;
    public float extraRadiusForDetection, explosionForce, extraRadiusForExplosion;

    public Color[] colors;
    public Sprite[] sprites;
}
