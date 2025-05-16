using UnityEngine;

public static class SpriteCutter
{
    public static (Sprite Left, Sprite Right) SplitHorizontally(this Sprite sprite)
    {
        Rect rect = sprite.rect;
        float halfWidth = rect.width / 2;
        
        Rect leftRect = new Rect(rect.x, rect.y, halfWidth, rect.height);
        Sprite leftSprite = Sprite.Create(sprite.texture, leftRect, new Vector2(1, 0.5f), sprite.pixelsPerUnit);
        
        Rect rightRect = new Rect(rect.x + halfWidth, rect.y, halfWidth, rect.height);
        Sprite rightSprite = Sprite.Create(sprite.texture, rightRect, new Vector2(0, 0.5f), sprite.pixelsPerUnit);

        return (leftSprite, rightSprite);
    }
}
