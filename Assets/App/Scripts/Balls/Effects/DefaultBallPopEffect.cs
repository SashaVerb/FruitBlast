using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DefaultBallPopEffect : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float explosionForce;
    [SerializeField] private float minRotationSpeed, maxRotationSpeed;
    [SerializeField] private float duration;
    [SerializeField] DefaultBallPart partPrefab;
    [SerializeField] ParticleSystem particles;
    
    public IEnumerator ExplosionEffect(Sprite spriteToCut)
    {
        //particles?.Play();
        
        var (left, right) = spriteToCut.SplitHorizontally();
        
        var leftPart = Instantiate(partPrefab, transform);
        leftPart.spriteRenderer.sprite = left;
        
        var rightPart = Instantiate(partPrefab, transform);
        rightPart.spriteRenderer.sprite = right;

        float angle = Random.Range(-90f, 90f) * Mathf.Deg2Rad;
        float verticalForce = Mathf.Sin(angle), horizontalForce = Mathf.Cos(angle);
        InitPart(leftPart, new Vector2(-horizontalForce, verticalForce));
        InitPart(rightPart, new Vector2(horizontalForce, verticalForce));
        
        yield return new WaitForSeconds(duration);
        
        // if(particles != null)
        //     yield return new WaitWhile(() => particles.isPlaying);
    }

    private void InitPart(DefaultBallPart part, Vector2 direction)
    {
        part.physics.Velocity = direction * explosionForce;
        part.physics.Gravity = gravity;
        part.physics.RotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        
        if (part.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            part.spriteRenderer.DOFade(0f, duration)
                .SetEase(Ease.OutCubic)
                .SetLink(spriteRenderer.gameObject);
        }
    }
}
