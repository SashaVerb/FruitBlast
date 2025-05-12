using UnityEngine;
using Random = UnityEngine.Random;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private BallsParameters ballsParameters;
    [SerializeField] private DefaultBall ballPrefab;
    [SerializeField] private CircleScaler circleScaler;

    public Ball Create()
    {
        var newBall = Instantiate(ballPrefab, transform);
        
        newBall.physicBody.Radius = Random.Range(ballsParameters.minRadius, ballsParameters.maxRadius);
        newBall.physicBody.Mass = Random.Range(ballsParameters.minMass, ballsParameters.maxMass);
        int id = Random.Range(0, ballsParameters.sprites.Length);

        var ballVisuals = ballsParameters.sprites[id];
        newBall.fruitSpriteRenderer.sprite = ballVisuals.fruitSprite;
        newBall.leftHalf.sprite = ballVisuals.leftHalf;
        newBall.rightHalf.sprite = ballVisuals.rightHalf;
        newBall.Id = id;

        circleScaler.RescaleCircle(newBall.physicBody);
        
        return newBall;
    }
}
