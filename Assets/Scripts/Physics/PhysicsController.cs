using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    [SerializeField] private PhysicsParameters parameters;

    private static List<PhysicCircle> bodies = new();
    private static List<PhysicLine> borders;

    private static List<CollisionInfo> collisionInfos = new();

    private void Awake()
    {
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
            bodies[i].AddVelocity(Vector2.down * parameters.gravity * Time.fixedDeltaTime);

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
        float radiusesLength = bodyA.Radius + bodyB.Radius;

        if (SquareDistance(positionA, positionB) < radiusesLength * radiusesLength)
        {
            float penetration = radiusesLength - Vector2.Distance(positionA, positionB);
            Vector2 normal = (positionB - positionA).normalized;
            collisionInfos.Add(new CollisionInfo(bodyA, bodyB, penetration, normal));
        }
    }

    private static float SquareDistance(Vector2 positionA, Vector2 positionB)
    {
        float deltaX = positionA.x - positionB.x, deltaY = positionA.y - positionB.y;

        return (deltaX * deltaX + deltaY * deltaY);
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

        if (speedOnNormal <= 0)
            return;

        float impulseMagnitude = (1 + parameters.restitution) * speedOnNormal / (bodyA.InverseMass + bodyB.InverseMass);

        if (speedOnNormal < parameters.minDeltaVelocity)
        {
            float bVelocityAlongNormal = -Vector2.Dot(bodyB.Velocity, normal);
            float aVelocityAlongNormal = Vector2.Dot(bodyA.Velocity, normal);
            if (aVelocityAlongNormal < 0)
            {
                bodyB.AddVelocity(normal * (aVelocityAlongNormal + bVelocityAlongNormal));
            }
            else if (bVelocityAlongNormal < 0)
            {
                bodyA.AddVelocity(-normal * (aVelocityAlongNormal + bVelocityAlongNormal));
            }
            else
            {
                bodyA.AddVelocity(-normal * (speedOnNormal - bVelocityAlongNormal));
                bodyB.AddVelocity(normal * bVelocityAlongNormal);
            }
        }
        else
        {
            bodyA.AddVelocity(-normal * impulseMagnitude * bodyA.InverseMass);
            bodyB.AddVelocity(normal * impulseMagnitude * bodyB.InverseMass);
        }
    }

    public void AddPhysicBody(PhysicCircle newBody)
    {
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

    public void DestroyAllBodies()
    {
        foreach(var body in bodies)
        {
            Destroy(body.gameObject);
        }
        bodies.Clear();
    }

    public static void RemoveBody(PhysicCircle body)
    {
        bodies.Remove(body);
    }

    public static PhysicCircle GetBodyAt(Vector2 position)
    {
        foreach (var body in bodies)
        {
            if (SquareDistance(position, body.transform.position) < body.Radius * body.Radius)
            {
                return body;
            }
        }
        return null;
    }

    public static List<PhysicCircle> GetBodiesInArea(Vector2 position, float radius)
    {
        List<PhysicCircle> result = new();
        float radiusesSquare;

        foreach (var body in bodies)
        {
            radiusesSquare = radius + body.Radius;
            radiusesSquare *= radiusesSquare;

            if (SquareDistance(position, body.transform.position) < radiusesSquare)
            {
                result.Add(body);
            }
        }

        return result;
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
