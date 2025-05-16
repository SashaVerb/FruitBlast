using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private GameFieldManager gameField;
    [SerializeField] private TurnsController turnsController;
    [SerializeField] private StartCountController startCountController;
    [SerializeField] private BallDestroyController ballDestroyController;

    private bool canPopBalls;
    
    private void Awake()
    {
        Events.OnGameOver.AddListener(OnGameOver);
    }
    
    private void Start()
    {
        StartCoroutine(OnStartRoutine());
    }

    private IEnumerator OnStartRoutine()
    {
        canPopBalls = false;
        gameField.Fill();
        
        yield return startCountController.StartCount(gameField.GetFallTime());

        canPopBalls = true;
    }
    
    private void OnGameOver()
    {
        StartCoroutine(OnGameOverRoutine());
    }
    
    private IEnumerator OnGameOverRoutine()
    {
        canPopBalls = false;
        gameField.CanAddBalls = false;
        gameField.BottomBorderActive = false;
        
        yield return gameField.WaitForBecomeEmpty();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && turnsController.CanMakeTurn() && canPopBalls)
        {
            PopBall();
        }
    }

    private void PopBall()
    {
        var body = PhysicsController.GetBodyAt(camera.ScreenToWorldPoint(Input.mousePosition));
        if (body != null && body.TryGetComponent<DefaultBall>(out DefaultBall defaultBall))
        {
            if (defaultBall.TryPop(out List<Ball> ballsToDestroy))
            {
                ballDestroyController.DestroyBalls(ballsToDestroy);
                turnsController.MinusOneTurn();
            }
        }
    }
}
