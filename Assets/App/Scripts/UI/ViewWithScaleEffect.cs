using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ViewWithScaleEffect : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float changeTime;
    [SerializeField] private float scaleValue;
    [Header("References")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI label;

    private Tween scaleTween;
    private Queue<string> textQueue = new();
    public void SetText(string text)
    {
        if (scaleTween != null && scaleTween.IsActive())
        {
            textQueue.Enqueue(text);
        }
        else
        {
            SetTextWithEffect(text);
        }
    }

    private void SetTextWithEffect(string text)
    {
        label.text = text;
        scaleTween = rectTransform.DOScale(scaleValue,changeTime)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(SetTextFromQueue);
    }

    public void SetTextWithoutEffect(string text)
    {
        label.text = text;
    }

    private void SetTextFromQueue()
    {
        if (textQueue.TryDequeue(out string text))
        {
            SetTextWithEffect(text);
        }
    }

    private void OnDestroy()
    {
        scaleTween?.Kill();
    }
}
