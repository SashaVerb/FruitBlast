using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressView : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float changeTime;
    [Header("References")]
    [SerializeField] private Slider slider;

    public UnityEvent<float> onValueChanged => slider.onValueChanged;
    public readonly UnityEvent onRichEnd = new();

    Sequence sliderSequence;
    Tween tween;
    private float debt = 0f, startMark = 0f;
    
    public void AddValue(float value)
    {
        debt += value;
        AddValueWithEffect(value);
    }

    private void AddValueWithEffect(float value)
    {
        if (sliderSequence != null && sliderSequence.IsActive())
        {
            sliderSequence.Kill();
            debt -= (startMark <= slider.value) ? (slider.value - startMark) : (1 - startMark +  slider.value);
        }
        
        startMark = slider.value;
        var sequence = DOTween.Sequence();
        for (var i = debt + slider.value; i >= 1f; i -= 1f)
        {
            sequence
                .Append(slider.DOValue(1f, changeTime))
                .AppendCallback(() => debt -= 1f);

            if (i >= 2f)
            {
                sequence.Append(slider.DOValue(0, changeTime));
            }
        }

        float finalValue = (debt + slider.value) % 1f;
        sequence
            .Append(slider.DOValue(finalValue, changeTime))
            .AppendCallback(() => debt = 0f);

        sliderSequence = sequence;
    }

    public IEnumerator SetValue(float value)
    {
        tween = slider.DOValue(value, changeTime);
        yield return tween.WaitForCompletion();
    }

    public IEnumerator LevelUp()
    {
        sliderSequence = DOTween.Sequence()
            .Append(slider.DOValue(1f, changeTime))
            .AppendCallback(onRichEnd.Invoke)
            .Append(slider.DOValue(0f, changeTime));
        
        yield return sliderSequence.WaitForCompletion();
    }
    
    public void SetValueWithoutEffect(float value)
    {
        slider.value = value;
    }

    private void OnDestroy()
    {
        sliderSequence?.Kill();
        tween?.Kill();
    }
}
