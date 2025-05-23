using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private PopUpView gameOverView;
    [SerializeField] private GameFieldManager gameField;
    
    private void Awake()
    {
        Events.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        GameplayManager.CanPopBalls = false;
        gameOverView.Show();
    }
    
    public void RestartGame()
    {
        StartCoroutine(RestartingRoutine());
    }
    
    private IEnumerator RestartingRoutine()
    {
        gameOverView.Hide();
        gameField.CanAddBalls = false;
        gameField.BottomBorderActive = false;
        
        yield return gameField.WaitForBecomeEmpty();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
