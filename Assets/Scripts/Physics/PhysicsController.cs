using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    [SerializeField] private Transform physicBodyParent;
    [SerializeField] private PhysicsParameters parameters;
    [SerializeField] private PhysicCircle physicCirclePrefab;

    private List<PhysicCircle> bodies;
    private List<PhysicLine> borders;

    private readonly List<CollisionInfo> collisionInfos = new();

    private void Awake()
    {
        bodies = new(physicBodyParent.GetComponentsInChildren<PhysicCircle>());
        InitBorders();
    }

    private void InitBorders()
    {
        borders = new(GetComponentsInChildren<PhysicLine>());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            bodies[i].AddImpulse(Vector2.down * parameters.gravity * Time.fixedDeltaTime);

            DetectBoundaryCollisions(bodies[i]);

            for (int j = i + 1; j < bodies.Count; j++)
            {
                DetectCollisionBetween(bodies[i], bodies[j]);
            }
        }

        ResolveCollisions();
    }

    private void DetectBoundaryCollisions(PhysicCircle body)
    {
        foreach (var border in borders)
        {
            Vector2 normal = border.Normal;
            Vector2 diff = body.transform.position - (Vector3)normal * body.Radius - border.transform.position;
            if(Vector2.Dot(normal, diff) < 0)
            {
                collisionInfos.Add(new CollisionInfo(border, body, -Vector2.Dot(diff, normal), normal));
            }
        }
    }

    private void DetectCollisionBetween(PhysicCircle bodyA, PhysicCircle bodyB)
    {
        Vector2 positionA = bodyA.transform.position;
        Vector2 positionB = bodyB.transform.position;
        float distance = Vector2.Distance(positionA, positionB);
        float penetration = (bodyA.Radius + bodyB.Radius) - distance;

        if (penetration > 0f)
        {
            Vector2 normal = (positionB - positionA).normalized;
            collisionInfos.Add(new CollisionInfo(bodyA, bodyB, penetration, normal));
        }
    }

    private void ResolveCollisions()
    {
        foreach (var collision in collisionInfos)
        {
            PhysicBody bodyA = collision.BodyA, bodyB = collision.BodyB;
            Vector2 normal = collision.Normal;

            Vector2 deltaVelocity = bodyA.Velocity - bodyB.Velocity;
            float speedOnNormal = Vector2.Dot(normal, deltaVelocity);

            ApplyVelocityChange(bodyA, bodyB, normal);
            ApplyPositionCorrection(bodyA, bodyB, collision.Penetration, normal);
        }

        collisionInfos.Clear();
    }

    private void ApplyPositionCorrection(PhysicBody bodyA, PhysicBody bodyB, float penetration, Vector2 normal)
    {
        Vector2 correction = normal * Mathf.Max(penetration - parameters.allowedPenetration, 0f) * parameters.correctionPercent * 0.5f;

        bodyA.AddMovementOffset(-correction);
        bodyB.AddMovementOffset(correction);
    }

    private void ApplyVelocityChange(PhysicBody bodyA, PhysicBody bodyB, Vector2 normal)
    {
        Vector2 deltaVelocity = bodyA.Velocity - bodyB.Velocity;
        float speedOnNormal = Vector2.Dot(normal, deltaVelocity);

        if (speedOnNormal < 0)
            return;

        float impulseMagnitude = (1 + parameters.restitution) * speedOnNormal / (bodyA.InverseMass + bodyB.InverseMass);

        if (speedOnNormal < parameters.minDeltaVelocity)
        {
            float bVelocityAlongNormal = -Vector2.Dot(bodyB.Velocity, normal);

            bodyA.AddImpulse(-normal * (speedOnNormal - bVelocityAlongNormal));
            bodyB.AddImpulse(normal * bVelocityAlongNormal);
        }
        else
        {
            bodyA.AddImpulse(-normal * impulseMagnitude * bodyA.InverseMass);
            bodyB.AddImpulse(normal * impulseMagnitude * bodyB.InverseMass);
        }
    }

    public void AddPhysicCircle()
    {
        var newBody = Instantiate(physicCirclePrefab, Vector3.up * 10f, Quaternion.identity, physicBodyParent);
        newBody.Radius = Random.Range(0.5f, 1.5f);
        bodies.Add(newBody);
    }

    public void ToggleBorder()
    {
        if (borders.Count == 0)
        {
            InitBorders();
        }
        else
        {
            borders.Clear();
        }
    }

    public void DestroyBodies()
    {
        foreach(var body in bodies)
        {
            Destroy(body.gameObject);
        }
        bodies.Clear();
    }

    private class CollisionInfo
    {
        public PhysicBody BodyA { get; }
        public PhysicBody BodyB { get; }
        public float Penetration { get; }
        public Vector2 Normal { get; }

        public CollisionInfo(PhysicBody bodyA, PhysicBody bodyB, float penetration, Vector2 normal)
        {
            BodyA = bodyA;
            BodyB = bodyB;
            Penetration = penetration;
            Normal = normal;
        }
    }
}
