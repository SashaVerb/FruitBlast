using UnityEngine;

public class GravityDirectionProvider
{
    public Vector2 GetGravityDirection()
    {
        Vector2 direction =  Vector2.down;
#if UNITY_IOS || UNITY_ANDROID
        direction.x += Input.acceleration.x;
#else
        direction.x += Input.GetAxis("Horizontal");
#endif
        return direction.normalized;
    }
}
