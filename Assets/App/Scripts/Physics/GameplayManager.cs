using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private PhysicsController physicsController;
    [SerializeField] private BallFactory ballFactory;
    [SerializeField] private Transform gameField;
    [SerializeField] private RefillIndicator refillIndicator;
    [SerializeField] private FillingParameters fillingParameters;

    private void Awake()
    {
        refillIndicator.OnGamefieldEmpty.AddListener(PlusOneBall);
        Events.OnBallDestroyed.AddListener(PlusOneBall);
    }

    private void Start()
    {
        FillGameField();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PopBall();
        }
    }

    private void PopBall()
    {
        var body = PhysicsController.GetBodyAt(camera.ScreenToWorldPoint(Input.mousePosition));
        if (body != null)
        {
            body.GetComponent<Ball>().Pop();
        }
    }

    private Ball CreateConfiguredBall()
    {
        var newBall = ballFactory.Create();
        float randomX = Random.Range(gameField.position.x - gameField.localScale.x * 0.5f, gameField.position.x + gameField.localScale.x * 0.5f),
            randomY = Random.Range(gameField.position.y + gameField.localScale.y * 0.5f, gameField.position.y + gameField.localScale.y * 0.5f + 3f);
        newBall.transform.position = new Vector3(randomX, randomY, 0);

        physicsController.AddPhysicBody(newBall.physicBody);

        return newBall;
    }

    public void PlusOneBall()
    {
        CreateConfiguredBall();
    }

    public void FillGameField()
    {
        StartCoroutine(FillGameFieldRoutine());
    }

    private IEnumerator FillGameFieldRoutine()
    {
        float area = gameField.localScale.x * (refillIndicator.transform.position.y - gameField.position.y + gameField.localScale.y * 0.5f), radius;
        while (area > 0)
        { 
            for(int i = 0; i < fillingParameters.groupCount; i++)
            {
                radius = CreateConfiguredBall().physicBody.Radius;
                area -= radius * radius * 4f;
            }
            
            yield return new WaitForSeconds(fillingParameters.interval);
        }
    }
}
