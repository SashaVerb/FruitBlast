using UnityEngine;

[CreateAssetMenu(fileName = "BallsParameters", menuName = "Scriptable Objects/Balls Parameters")]
public class BallsParameters : ScriptableObject
{
    public float minMass, maxMass;
    public float minRadius, maxRadius;
    public float extraRadiusForDetection, explosionForce, extraRadiusForExplosion;
    
    public BallVisuals[] sprites;

    [System.Serializable]
    public class BallVisuals
    {
        public Sprite fruitSprite, leftHalf, rightHalf;
        public Color color;
    }
}
