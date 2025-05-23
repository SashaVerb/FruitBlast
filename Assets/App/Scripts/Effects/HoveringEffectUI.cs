using DG.Tweening;
using UnityEngine;

public class HoveringEffectUI : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private float height;
    [SerializeField] private Ease ease = Ease.InOutCubic;
    
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = startPosition;
        transform.DOMoveY(height, period)
            .SetRelative()
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
