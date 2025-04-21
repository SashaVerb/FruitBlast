using UnityEngine;
public class PhysicBody : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [SerializeField] bool IsStatic = false;

    private Vector3 velocity, movementOffset = Vector3.zero;
    public float Mass => mass;
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

    public void AddImpulse(Vector3 impulse)
    {
        if(!IsStatic)
            velocity += impulse;
    }

    public void AddMovementOffset(Vector3 offset)
    {
        movementOffset += offset;
    }

    private void FixedUpdate()
    {
        if (!IsStatic)
            Move();
    }
    private void Move()
    {
        Vector3 totalMovement = velocity + movementOffset;

        if (totalMovement.magnitude > 0.01f)
        {
            transform.position += totalMovement;
        }

        movementOffset = Vector3.zero;
    }
}