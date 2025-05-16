using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AppearWithUpscaleEffect : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool useParticleSystem = false;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform model;
    private void Awake()
    {
        model.localScale = Vector3.zero;
        StartCoroutine(AppearRoutine());
    }

    private IEnumerator AppearRoutine()
    {
        var scaleTween = model
            .DOScale(1, duration)
            .SetEase(Ease.OutBack)
            .SetLink(gameObject);
        
        if(useParticleSystem)
            particle.Play();
        
        yield return scaleTween.WaitForCompletion();
        
        if(useParticleSystem)
            yield return new WaitWhile(() => particle.isPlaying);
    }
}
