using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteImage : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }
}
