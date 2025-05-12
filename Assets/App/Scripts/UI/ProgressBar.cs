using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int neededProgress;
    [SerializeField] TextMeshProUGUI percentLabel;
    [SerializeField] TextMeshProUGUI levelLabel;

    private int currentProgress;
    private int level;

    private void Awake()
    {
        currentProgress = 0;
        level = 1;
        Events.OnBallDestroyed.AddListener(AddProgress);

        UpdateWholeUI();
    }

    private void AddProgress()
    {
        currentProgress++;
        if (currentProgress >= neededProgress)
        {
            currentProgress -= neededProgress;
            level++;
            levelLabel.text = level.ToString();
        }

        float progress = (float)currentProgress / neededProgress;
        slider.value = progress;
        percentLabel.text = ((int)(progress * 100)).ToString() + "%";
    }

    private void UpdateWholeUI()
    {
        float progress = currentProgress / neededProgress;

        levelLabel.text = level.ToString();
        percentLabel.text = ((int)progress).ToString() + "%";
        slider.value = progress;
    }
}
