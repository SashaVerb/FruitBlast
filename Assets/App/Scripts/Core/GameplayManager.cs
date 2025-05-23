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

    public static bool CanPopBalls { get; set; }
    
    private void Start()
    {
        StartCoroutine(OnStartRoutine());
    }

    private IEnumerator OnStartRoutine()
    {
        CanPopBalls = false;
        gameField.Fill();
        
        yield return startCountController.StartCount(gameField.GetFallTime());

        CanPopBalls = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && turnsController.CanMakeTurn() && CanPopBalls)
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
