using UnityEngine;
using Random = UnityEngine.Random;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private BallPhysicParameters ballsPhysic;
    [SerializeField] private DefaultBall defaultBallPrefab;
    [SerializeField] private CircleScaler circleScaler;

    public Ball CreateDefaultBall()
    {
        return AddBallToField(defaultBallPrefab);
    }

    public Ball AddBallToField(Ball ball)
    {
        Ball newBall = Instantiate(ball, transform);
        
        return ConfigureBall(newBall);
    }
    
    private Ball ConfigureBall(Ball newBall)
    {
        newBall.physicBody.Radius = Random.Range(ballsPhysic.minRadius, ballsPhysic.maxRadius);
        newBall.physicBody.Mass = Random.Range(ballsPhysic.minMass, ballsPhysic.maxMass);

        circleScaler.RescaleCircle(newBall.physicBody);
        
        return newBall;
    }
}
