using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameFieldSize", menuName = "Scriptable Objects/Game Field Size Parameters")]
public class GameFieldParameters : ScriptableObject
{
    public Vector2Int aspect;
    public float topOffset;
    public float minBottomOffset;
    public float minRightOffset;
    public float minLeftOffset;
    private float aspectValue;

    private void OnEnable()
    {
        aspectValue = (float)aspect.x / aspect.y;
    }

    private void OnValidate()
    {
        aspectValue = (float)aspect.x / aspect.y;
    }

    public void GetCenterAndSize(Bounds bounds, out Vector3 center, out Vector3 size)
    {
        float height = bounds.size.y;
        float width = bounds.size.x;
        float freeHeight = height - topOffset - minBottomOffset, freeWidth = width - minRightOffset - minLeftOffset;

        if (freeHeight * aspectValue > freeWidth)
        {
            size = new Vector3(freeWidth, freeWidth / aspectValue);
        }
        else
        {
            size = new Vector3(freeHeight * aspectValue, freeHeight);
        }

        center = new Vector3(
            minLeftOffset + (freeWidth - width) * 0.5f,
            (height - size.y) * 0.5f - topOffset
        );
    }
}
