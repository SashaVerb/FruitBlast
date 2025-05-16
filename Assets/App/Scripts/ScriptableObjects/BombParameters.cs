using UnityEngine;

[CreateAssetMenu(fileName = "BombParameters", menuName = "Scriptable Objects/Balls/Bomb Parameters")]
public class BombParameters : ScriptableObject
{
    public float destroyRadius, explosionExtraRadius, explosionForce;
}
