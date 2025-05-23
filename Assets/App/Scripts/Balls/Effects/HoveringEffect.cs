using DG.Tweening;
using UnityEngine;

public class HoveringEffect : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float period;
    
    private Tween hoveringTween;
    private Vector3 startPosition;
    
    public void PlayEffect()
    {
        hoveringTween = transform
            .DOMove(transform.position + Vector3.up * amplitude, period)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopEffect()
    {
        hoveringTween.Kill();
    }

    private void OnDestroy()
    {
        hoveringTween.Kill();
    }
}
