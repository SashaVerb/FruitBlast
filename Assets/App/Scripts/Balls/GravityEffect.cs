using System;
using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private Vector2 velocity;

    public Vector2 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public float Gravity { get; set; } = 1f;

    private void FixedUpdate()
    {
        velocity.y -= Gravity * Time.fixedDeltaTime;;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
