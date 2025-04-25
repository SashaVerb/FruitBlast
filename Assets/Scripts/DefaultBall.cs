using System.Collections.Generic;
using UnityEngine;

public class DefaultBall : Ball
{
    [SerializeField] private int minNeighboursToPop;
    public override void Pop()
    {
        HashSet<Ball> sameBalls = new();
        Queue<Ball> toCheckNext = new();

        sameBalls.Add(this);
        toCheckNext.Enqueue(this);

        while(toCheckNext.TryDequeue(out Ball ball))
        {
            foreach (var neighbour in ball.GetNeighbours())
            {
                if (neighbour.Id == Id && !sameBalls.Contains(neighbour))
                {
                    sameBalls.Add(neighbour);
                    toCheckNext.Enqueue(neighbour);
                }
            }
        }

        if (sameBalls.Count >= minNeighboursToPop)
        {
            foreach (var ball in sameBalls)
            {
                Destroy(ball.gameObject);
            }
        }
    }
}
