using System;
using System.Collections;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private PerksView perksView;
    [SerializeField] private ProgressView view;
    [SerializeField] private ViewWithScaleEffect levelLabel;
    [SerializeField] private int maxProgress;
    private int currentProgress = 0, level = 0;
    
    public float Progress => (float)currentProgress / maxProgress;
    public int Level => level;
    private bool ignore = false;
    
    private Coroutine slidingCoroutine, levelUpCoroutine;
    
    private void Awake()
    {
        Events.OnGameOver.AddListener(Freeze);
    }

    private void Freeze()
    {
        ignore = true;
        view.enabled = false;
    }

    public void AddProgress(int progress)
    {
        if (ignore) return;
        
        currentProgress += progress;
        
        if (slidingCoroutine != null)
            StopCoroutine(slidingCoroutine);
        
        if (currentProgress >= maxProgress)
        {
            if(levelUpCoroutine == null)
                levelUpCoroutine = StartCoroutine(LevelUpRoutine());
        }
        else
        {
            slidingCoroutine = StartCoroutine(view.SetValue(Progress));
        }
    }

    private IEnumerator LevelUpRoutine()
    {
        while (currentProgress >= maxProgress)
        {
            yield return view.LevelUp();
            currentProgress -= maxProgress;
            AddLevel();
            GameplayManager.CanPopBalls = false;
            perksView.ShowView(level);
            yield return new WaitWhile(() => perksView.IsShowing);
            GameplayManager.CanPopBalls = true;
        }
        
        levelUpCoroutine = null;
        yield return view.SetValue(Progress);
    }

    private void AddLevel()
    {
        level++;
        levelLabel.SetText(level.ToString());
    }
}
