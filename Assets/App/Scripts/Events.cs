using UnityEngine.Events;

public static class Events
{
    public static readonly UnityEvent OnBallDestroyed = new();
    public static readonly UnityEvent OnGameOver = new();
}
