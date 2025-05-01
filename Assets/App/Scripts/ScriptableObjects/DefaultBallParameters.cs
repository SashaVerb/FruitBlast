using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBallParameters", menuName = "Scriptable Objects/Balls/Default Ball")]
public class DefaultBallParameters : ScriptableObject
{
    public int minNeighboursToPop;
    public float explosionDelay;

}
