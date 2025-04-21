using UnityEngine;

public class CameraToBorder : MonoBehaviour, IBoundsProvider
{
    [SerializeField] private new Camera camera;

    public Bounds GetBounds()
    {
        float height = camera.orthographicSize * 2f;
        float width = camera.aspect * height;
        Vector2 size = new Vector2(width, height);

        return new Bounds(transform.position, size);
    }
}
