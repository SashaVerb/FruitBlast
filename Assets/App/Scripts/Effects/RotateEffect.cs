using DG.Tweening;
using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private bool toTheRight;

    private void OnEnable()
    {
        transform.DORotate(Vector3.forward * 360 * (toTheRight ? 1f : -1f), period, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
