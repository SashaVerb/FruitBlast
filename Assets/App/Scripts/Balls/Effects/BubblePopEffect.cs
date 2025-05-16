using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BubblePopEffect : MonoBehaviour
{
    [SerializeField] float popDuration;
    [SerializeField] Transform bubbleSpriteParent;
    [SerializeField] ParticleSystem particle;

    public IEnumerator PlayEffect()
    {
        Tween scaleTween = bubbleSpriteParent.DOScale(Vector3.zero, popDuration)
            .SetEase(Ease.InBack)
            .SetLink(gameObject);
        particle.Play();
        
        yield return scaleTween.WaitForCompletion();
        
        yield return new WaitWhile(() => particle.IsAlive());
    }
}
