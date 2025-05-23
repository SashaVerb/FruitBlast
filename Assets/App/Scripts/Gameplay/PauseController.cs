using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private PopUpView popUpView;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameplayManager.CanPopBalls = false;
        popUpView.Show();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameplayManager.CanPopBalls = true;
        popUpView.Hide();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        popUpView.Hide();
        Events.OnGameOver.Invoke();
    }
}
