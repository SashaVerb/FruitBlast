using UnityEngine;

[RequireComponent(typeof(PhysicCircle))]
public class CircleGizmoDrawer : MonoBehaviour
{
    private PhysicCircle circle;

    private void OnDrawGizmos()
    {
        if (circle == null)
        {
            circle = GetComponent<PhysicCircle>();
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circle.Radius);
    }
}
