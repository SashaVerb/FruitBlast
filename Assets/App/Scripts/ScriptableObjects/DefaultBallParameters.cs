using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBallParameters", menuName = "Scriptable Objects/Balls/Default Ball")]
public class DefaultBallParameters : ScriptableObject
{
    public int minNeighboursToPop;
    public float explosionChainDelay, extraRadiusForDetection, explosionForce, explosionExtraRadius;

    public BallVisuals[] sprites;

    [System.Serializable]
    public class BallVisuals
    {
        public Sprite fruitSprite;
    }
}
