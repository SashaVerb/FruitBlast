using DG.Tweening;
using UnityEngine;

public class ScaleEffectUI : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private float amplitude;
    [SerializeField] private Ease ease = Ease.InOutCubic;
    
    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = startScale;
        transform.DOScale(amplitude, period)
            .SetRelative()
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
