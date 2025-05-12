using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameFieldManager : MonoBehaviour
{
    [SerializeField] private BallFactory ballFactory;
    [SerializeField] private Transform gameField;
    [SerializeField] private ObjectAboveIndicator refillIndicator;
    [SerializeField] private ObjectAboveIndicator gameFieldEmptyIndicator;
    [SerializeField] private FillingParameters fillingParameters;
    [SerializeField] private PhysicLine bottomBorder;
    [SerializeField] private PhysicsController physicsController;

    public bool CanAddBalls { get; set; } = true;
    private bool bottomBorderActive = true;
    
    public bool BottomBorderActive
    {
        get => bottomBorderActive;
        set
        {
            bottomBorderActive = value;
            bottomBorder.enabled = value;
        }
    }

    private void Awake()
    {
        refillIndicator.OnNoObjectsAbove.AddListener(() => CreateConfiguredBall());
        Events.OnBallDestroyed.AddListener(() => CreateConfiguredBall());
    }
    
    public Ball CreateConfiguredBall()
    {
        if(!CanAddBalls)
            return null;
        
        var newBall = ballFactory.Create();
        float randomX = Random.Range(gameField.position.x - gameField.localScale.x * 0.5f,
                gameField.position.x + gameField.localScale.x * 0.5f),
            randomY = Random.Range(gameField.position.y + gameField.localScale.y * 0.5f, gameField.position.y + gameField.localScale.y * 0.5f + 3f);
        newBall.transform.position = new Vector3(randomX, randomY, 0);
        
        return newBall;
    }
    
    public void Fill()
    {
        StartCoroutine(FillingRoutine());
    }

    private IEnumerator FillingRoutine()
    {
        float area = gameField.localScale.x *
                     (refillIndicator.transform.position.y - gameField.position.y + gameField.localScale.y * 0.5f),
            radius;
        while (area > 0)
        {
            for (int i = 0; i < fillingParameters.groupCount; i++)
            {
                radius = CreateConfiguredBall().physicBody.Radius;
                area -= radius * radius * 4f;
            }

            yield return new WaitForSeconds(fillingParameters.interval);
        }
    }

    public float GetFallTime()
    {
        float distance = gameField.localScale.y;
        float fallTime = Mathf.Sqrt(distance * 2f / physicsController.parameters.gravity);
        return fallTime;
    }

    public IEnumerator WaitForBecomeEmpty()
    {
        yield return new WaitWhile(() => gameFieldEmptyIndicator.HasObjects);
    }
}
