using UnityEngine;
using Random = UnityEngine.Random;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private BallPhysicParameters ballsPhysic;
    [SerializeField] private DefaultBall defaultBallPrefab;
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private HorizontalBomb horizontalBombPrefab;
    [SerializeField] private BallTypes ballTypes;
    [SerializeField] private CircleScaler circleScaler;
    [SerializeField] private GameObject[] subscribers;

    public Ball CreateDefaultBall()
    {
        Ball newBall = Instantiate(defaultBallPrefab, transform);
            
        return ConfigureBall(newBall);
    }
    
    public Ball CreateBomb(float radius)
    {
        Bomb newBall = Instantiate(bombPrefab, transform);
        newBall.Radius = radius;
        return ConfigureBall(newBall);
    }

    public Ball CreateHorizontalBall(float length)
    {
        HorizontalBomb newBall = Instantiate(horizontalBombPrefab, transform);
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
