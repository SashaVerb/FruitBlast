using UnityEngine;

public class GameFieldScaler : MonoBehaviour
{
    [SerializeField] private GameFieldParameters fieldParameters;
    [SerializeField] private CameraToBorder boundsProvider;
    [SerializeField] private CircleScaler circleScaler;

    private void Awake()
    {
        RescaleGameField();
    }

    private void Update()
    {
        RescaleGameField();
    }

    private void OnValidate()
    {
        RescaleGameField();
    }

    private void RescaleGameField()
    {
        fieldParameters.GetCenterAndSize(boundsProvider.GetBounds(), out Vector3 center, out Vector3 size);
        transform.position = center;
        transform.localScale = size;

        circleScaler.RescaleAllCircles(Mathf.Min(size.x, size.y));
    }
}
