using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PerksViewEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private RectTransform starLevel;
    [SerializeField] private RectTransform newLevelText;
    [SerializeField] private RectTransform tip;
    [SerializeField] private RectTransform[] perkSlots;
    [SerializeField] private Image background;
    
    private float backgroundActiveAlpha;
    private Sequence sequence;
    private void Awake()
    {
        backgroundActiveAlpha = background.color.a;
    }

    public IEnumerator ShowEffect()
    {
        HideEverithingWithoutEffect();

        if (sequence != null && sequence.IsActive())
        {
            sequence.Kill();
        }
        
        sequence = DOTween.Sequence()
            .Append(background.DOFade(backgroundActiveAlpha, duration))
            .Append(newLevelText.DOScale(1f, duration).SetEase(Ease.OutBack))
            .Append(starLevel.DOScale(1f, duration).SetEase(Ease.OutBack))
            .Join(tip.DOScale(1f, duration));

        foreach (var perk in perkSlots)
        {
            sequence.Join(perk.DOScale(1f, duration).SetEase(Ease.OutBack));
        }
        
        yield return sequence.WaitForCompletion();
    }

    private void HideEverithingWithoutEffect()
    {
        starLevel.localScale = Vector3.zero;
        tip.localScale = Vector3.zero;
        newLevelText.localScale = Vector3.zero;
        foreach (var perk in perkSlots)
        {
            perk.localScale = Vector3.zero;
        }
        
        var color = background.color;
        color.a = 0;
        background.color = color;
    }
    
    public IEnumerator HideEffect()
    {
        if (sequence != null && sequence.IsActive())
        {
            sequence.Kill();
        }

        sequence = DOTween.Sequence();
            
        foreach (var perk in perkSlots)
        {
            sequence.Join(perk.DOScale(0f, duration).SetEase(Ease.InBack));
        }
        
        sequence.Append(starLevel.DOScale(0f, duration).SetEase(Ease.InBack))
            .Join(tip.DOScale(0f, duration))
            .Append(newLevelText.DOScale(0f, duration).SetEase(Ease.InBack))
            .Append(background.DOFade(0f, duration));
        
        
        yield return sequence.WaitForCompletion();
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
