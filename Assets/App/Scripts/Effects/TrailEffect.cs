using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailEffect : MonoBehaviour, IStopEffect
{
    private TrailRenderer trail;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public IEnumerator StopEffect()
    {
        trail.emitting = false;

        yield return new WaitForSeconds(trail.time);
    }
}
