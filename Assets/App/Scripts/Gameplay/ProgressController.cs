using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private PerksView perksView;
    [SerializeField] private ProgressView view;
    [SerializeField] private ViewWithScaleEffect levelLabel;
    
    private int currentProgress = 0, maxProgress = 10, level = 0;
    
    public float Progress => (float)currentProgress / maxProgress;
    public int Level => level;
    
    private Coroutine levelUpCoroutine, slidingCoroutine;
    public void AddProgress(int progress)
    {
        currentProgress += progress;
        if (currentProgress >= maxProgress)
        {
            if (levelUpCoroutine != null)
                StopCoroutine(levelUpCoroutine);
            levelUpCoroutine = StartCoroutine(LevelUpRoutine());
        }
        else
        {
            if (slidingCoroutine != null)
                StopCoroutine(slidingCoroutine);
            slidingCoroutine = StartCoroutine(view.SetValue(Progress));
        }
    }

    private IEnumerator LevelUpRoutine()
    {
        while (currentProgress >= maxProgress)
        {
            yield return view.LevelUp();
            // perksView.ShowView(level);
            // yield return new WaitWhile(() => perksView.IsShowing);
            currentProgress -= maxProgress;
            AddLevel();
        }
        
        yield return view.SetValue(Progress);
    }

    private void AddLevel()
    {
        level++;
        levelLabel.SetText(level.ToString());
    }
}
