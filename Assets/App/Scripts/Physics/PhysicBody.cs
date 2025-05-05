using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [SerializeField] private bool isStatic = false;

    private Vector3 velocity, movementOffset = Vector3.zero;
    public float Mass { get => mass; set => mass = value; }
    public float InverseMass
    {
        get
        {
            if (mass == 0f)
                return 0f;
            else
                return 1 / mass;
        }
    }

    public Vector3 Velocity => velocity;

    public bool IsStatic { get => isStatic; set => isStatic = value;}

    public void AddVelocity(Vector3 impulse)
    {
        if (!isStatic)
            velocity += impulse;
    }

    public void AddMovementOffset(Vector3 offset)
    {
        movementOffset += offset;
    }

    private void FixedUpdate()
    {
        if (!isStatic)
            Move();
    }
    private void Move()
    {

        Vector3 totalMovement = velocity + movementOffset;

        if (movementOffset.magnitude > velocity.magnitude)
        {
            velocity = movementOffset;
        }

        if (totalMovement.magnitude > 0.01f)
        {
            transform.position += totalMovement;
        }

        movementOffset = Vector3.zero;
    }
}