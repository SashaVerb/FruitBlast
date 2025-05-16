using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextWithFadeEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float height;
    
    private TextMeshProUGUI label;
    private RectTransform rectTransform;
    private Vector2 initPosition;
    private Color transparentColor, normalColor;
    
    private Sequence sequence;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        label = GetComponent<TextMeshProUGUI>();
        
        initPosition = rectTransform.anchoredPosition;
        transparentColor = normalColor = label.color;
        transparentColor.a = 0f;
        normalColor.a = 1f;
        
        label.color = transparentColor;
        Refresh();
    }

    public void ShowText(string text)
    {
        label.text = text;
        label.color = normalColor;
        if (sequence != null && sequence.IsActive())
        {
            sequence.Kill();
            Refresh();
        }
        
        sequence = DOTween.Sequence()
            .Append(rectTransform.DOAnchorPosY(initPosition.y + height, duration))
            .Join(label.DOFade(0f, duration))
            .OnComplete(Refresh);
    }

    private void Refresh()
    {
        rectTransform.anchoredPosition = initPosition;
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
