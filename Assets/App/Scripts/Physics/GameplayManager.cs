using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private BallFactory ballFactory;
    [SerializeField] private Transform gameField;
    [SerializeField] private ObjectAboveIndicator refillIndicator;
    [SerializeField] private ObjectAboveIndicator gameFieldEmptyIndicator;
    [SerializeField] private FillingParameters fillingParameters;
    [SerializeField] private TurnsController turnsController;
    
    private void Awake()
    {
        refillIndicator.OnNoObjectsAbove.AddListener(PlusOneBall);
        Events.OnBallDestroyed.AddListener(PlusOneBall);
        Events.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        refillIndicator.OnNoObjectsAbove.RemoveListener(PlusOneBall);
        gameFieldEmptyIndicator.OnNoObjectsAbove.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void Start()
    {
        FillGameField();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && turnsController.CanMakeTurn())
        {
            PopBall();
        }
    }

    private void PopBall()
    {
        var body = PhysicsController.GetBodyAt(camera.ScreenToWorldPoint(Input.mousePosition));
        if (body != null)
        {
            if (body.GetComponent<Ball>().Pop())
            {
                turnsController.MinusOneTurn();
            }
        }
    }

    private Ball CreateConfiguredBall()
    {
        var newBall = ballFactory.Create();
        float randomX = Random.Range(gameField.position.x - gameField.localScale.x * 0.5f,
                gameField.position.x + gameField.localScale.x * 0.5f),
            randomY = Random.Range(gameField.position.y + gameField.localScale.y * 0.5f, gameField.position.y + gameField.localScale.y * 0.5f + 3f);
        newBall.transform.position = new Vector3(randomX, randomY, 0);
        
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
