using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MovingToTargetInArc : MonoBehaviour
{
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [SerializeField] private float sideAmplitude;
    [SerializeField] private int pointsCount;
    [SerializeField] private Ease ease = Ease.InSine;
    private Transform target;

    public readonly UnityEvent onRichTarget = new();
    
    public void SetTarget(Vector3 target)
    {
        Vector3[] points = new Vector3[(pointsCount + 1) * 3];
        Vector3 direction = (target - transform.position).normalized;
        Vector3 perpendicular = new Vector3(direction.y, -direction.x);
        float sign = Mathf.Sign(Random.value - 0.5f);

        Vector3 lastWayPoint = transform.position;
        for (int i = 0; i <= pointsCount; i++)
        {
            Vector3 wayPoint = Vector3.Lerp(transform.position, target, (i + 1f) / (pointsCount + 1));
            wayPoint += perpendicular * (sign * sideAmplitude);
            sign *= -1f;
            int index = i * 3;
            points[index] = wayPoint;
            points[index + 1] = lastWayPoint + direction;
            points[index + 2] = wayPoint - direction;
            
            lastWayPoint = wayPoint;
        }

        transform.DOPath(points.ToArray(), Random.Range(minDuration, maxDuration), PathType.CubicBezier, PathMode.Sidescroller2D)
            .SetEase(ease)
            .OnComplete(onRichTarget.Invoke);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
