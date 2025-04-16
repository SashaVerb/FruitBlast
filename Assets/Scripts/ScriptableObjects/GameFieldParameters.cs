using UnityEngine;

[CreateAssetMenu(fileName = "GameFieldSize", menuName = "Scriptable Objects/Game Field Size Parameters")]
public class GameFieldParameters : ScriptableObject
{
    public Vector2Int aspect;
    public float topOffset;
    public float minBottomOffset;
    public float minRightOffset;
    public float minLeftOffset;
    public float aspectValue { get; private set; }

    GameFieldParameters()
    {
        aspectValue = (float)aspect.x / aspect.y;
    }

    private void OnValidate()
    {
        aspectValue = (float)aspect.x / aspect.y;
    }

    public void GetCenterAndSize(out Vector3 center, out Vector3 size)
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = Camera.main.aspect * height;
        float freeHeight = height - topOffset - minBottomOffset, freeWidth = width - minRightOffset - minLeftOffset;

        center = new Vector3(
            minLeftOffset + (freeWidth - width) * 0.5f,
            minBottomOffset + (freeHeight - height) * 0.5f
            );

        if (freeHeight * aspectValue > freeWidth)
        {
            size = new Vector3(freeWidth, freeWidth / aspectValue);
        }
        else
        {
            size = new Vector3(freeHeight * aspectValue, freeHeight);
        }
    }
}
