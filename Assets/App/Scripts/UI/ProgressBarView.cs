using TMPro;
using UnityEngine;

public class ProgressBarView : MonoBehaviour
{
    [SerializeField] int neededProgress;
    [SerializeField] TextMeshProUGUI percentLabel;
    [SerializeField] ProgressView progressBar;
    [SerializeField] ViewWithScaleEffect levelView;

    private int currentProgress;
    private int level;

    private void Awake()
    {
        progressBar.onValueChanged.AddListener(RedrawUI);
        
        currentProgress = 0;
        level = 1;

        levelView.SetTextWithoutEffect(level.ToString());
        progressBar.SetValueWithoutEffect(0f);
    }

    public void AddProgress()
    {
        currentProgress++;
        if (currentProgress >= neededProgress)
        {
            currentProgress -= neededProgress;
            level++;
            SetLevel(level);
        }

        float progress = (float)currentProgress / neededProgress;
        progressBar.AddValue(1f / neededProgress);
    }

    public void SetLevel(int level)
    {
        levelView.SetText(level.ToString());
    }

    private void RedrawUI(float value)
    {
        percentLabel.text = ((int)(value * 100)).ToString() + "%";
    }
}
