using System;
using System.Drawing;
using UnityEngine;

public class GameFieldScaler : MonoBehaviour
{
    [SerializeField] private GameFieldParameters fieldParameters;
    private IBoundsProvider boundsProvider;

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
        if (boundsProvider == null)
        {
            boundsProvider = GetComponent<IBoundsProvider>();
        }

        fieldParameters.GetCenterAndSize(boundsProvider.GetBounds(), out Vector3 center, out Vector3 size);
        transform.position = center;
        transform.localScale = size;
    }
}
