using UnityEngine;

public class SimplePhysics : MonoBehaviour
{
    private Vector2 velocity;

    public Vector2 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public float Gravity { get; set; } = 1f;

    public float RotationSpeed { get; set; }
    private void FixedUpdate()
    {
        velocity.y -= Gravity * Time.fixedDeltaTime;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, velocity.x * RotationSpeed * Time.fixedDeltaTime);
    }
}
