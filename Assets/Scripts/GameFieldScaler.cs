using UnityEngine;

public class GameFieldScaler : MonoBehaviour
{
    [SerializeField] private GameFieldParameters fieldParameters;

    private float aspectValue;

    private void Start()
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
        fieldParameters.GetCenterAndSize(out Vector3 center, out Vector3 size);
        transform.position = center;
        transform.localScale = size;
    }
}
