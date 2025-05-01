using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private BallsParameters ballsParameters;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private CircleScaler circleScaler;

    public Ball Create()
    {
        var newBall = Instantiate(ballPrefab, transform);

        newBall.physicBody.Radius = Random.Range(ballsParameters.minRadius, ballsParameters.maxRadius);
        newBall.physicBody.Mass = Random.Range(ballsParameters.minMass, ballsParameters.maxMass);
        int id = Random.Range(0, ballsParameters.sprites.Length);

        newBall.spriteRenderer.sprite = ballsParameters.sprites[id];
        newBall.Id = id;

        circleScaler.RescaleCircle(newBall.physicBody);

        return newBall;
    }
}
