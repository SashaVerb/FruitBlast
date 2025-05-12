using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultBallPopEffect : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float explosionForse;
    [SerializeField] private float duration;
    [SerializeField] GravityEffect[] explosionParts;
    [SerializeField] ParticleSystem particles;

    private void Awake()
    {
        foreach (var part in explosionParts)
        {
            part.Gravity = gravity;
            part.enabled = false;
        }
    }
    
    public IEnumerator ExplosionEffect()
    {
        //particles?.Play();
        
        foreach (var part in explosionParts)
        {
            part.Velocity = (part.transform.position - transform.position).normalized * explosionForse;
            part.enabled = true;
            if (part.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.DOFade(0f, duration)
                    .SetLink(spriteRenderer.gameObject);
            }
        }
        
        yield return new WaitForSeconds(duration);
        
        // if(particles != null)
        //     yield return new WaitWhile(() => particles.isPlaying);
    }
}
