using UnityEngine;

public class BorderGizmoDrawer : MonoBehaviour
{
    [SerializeField] float length = 1f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        float angle = (transform.eulerAngles.z - 90f) * Mathf.Deg2Rad;
        Vector3 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Gizmos.DrawLine(transform.position + direction * length, transform.position - direction * length);
    }
}
