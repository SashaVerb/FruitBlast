using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image background;
    [SerializeField] private RectTransform window;

    float initialAlpha;
    Sequence sequence;
    
    private void Awake()
    {
        initialAlpha = background.color.a;
        canvas.gameObject.SetActive(false);
    }

    private void HideEverythingWithoutEffect()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0.0f);
        window.localScale = Vector3.zero;
    }
    
    public void Show()
    {
        canvas.gameObject.SetActive(true);
        HideEverythingWithoutEffect();
        
        sequence?.Kill();
        sequence = DOTween.Sequence()
            .Append(background.DOFade(initialAlpha, duration))
            .Append(window.DOScale(1f, duration))
            .SetUpdate(true);
    }

    public void Hide()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence()
            .Append(window.DOScale(0f, duration))
            .Append(background.DOFade(0f, duration))
            .OnComplete(() => canvas.gameObject.SetActive(false));
    }
}
