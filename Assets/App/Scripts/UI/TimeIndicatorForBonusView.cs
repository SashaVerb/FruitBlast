using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeIndicatorForBonusView : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectMask2D rectMask;

    private Tween tween;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowForSeconds(float seconds)
    {
        gameObject.SetActive(true);
        var target  = new Vector4(rectTransform.rect.width, 0, 0, 0);
        rectMask.padding = Vector4.zero;
        
        tween?.Kill();
        
        tween = DOTween.To(() => rectMask.padding, x => rectMask.padding = x, target, seconds)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    private void OnDisable()
    {
        tween?.Kill();
    }
}
