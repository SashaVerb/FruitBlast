using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DestroyWithDownScaleEffect : MonoBehaviour
{
    [SerializeField] private float scaleDuration;
    
    [SerializeField] private bool useParticleSystem = false;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform model;
    
    [SerializeField] private GameObject[] effectsToWait;
    
    [HideInInspector] 
    public readonly UnityEvent OnDestroyMoment = new UnityEvent();
    
    private List<IStopEffect> stopEffects = new();
    
    private void Awake()
    {
        foreach (var effect in effectsToWait)
        {
            if (effect.TryGetComponent(out IStopEffect stopEffect))
            {
                stopEffects.Add(stopEffect);
            }
        }
    }

    public void Destroy()
    {
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        List<Coroutine> effectCoroutines = new List<Coroutine>(stopEffects.Count);
    
        foreach (var effect in stopEffects)
        {
            Coroutine coroutine = StartCoroutine(effect.StopEffect());
            effectCoroutines.Add(coroutine);
        }
        
        foreach (var coroutine in effectCoroutines)
        {
            yield return coroutine;
        }
        
        var scaleTween = model
            .DOScale(0, scaleDuration)
            .SetEase(Ease.InBack)
            .SetLink(gameObject);
        
        if(useParticleSystem)
            particle.Play();
        
        yield return scaleTween.WaitForCompletion();
        
        if(useParticleSystem)
            yield return new WaitWhile(() => particle.isPlaying);
        
        OnDestroyMoment.Invoke();
        Destroy(gameObject);
    }
}
