using System.Collections;
using UnityEngine;

public class QuickFinger: AddingBallOnDestroyPerk
{
    [SerializeField] private float startDuration;
    [SerializeField] private float increaseDuration;
    [Header("View")]
    [SerializeField] private TimeIndicatorForBonusView view;
    
    private Parameters parameters;
    public bool IsActive { get; private set; } = false;
    private Coroutine coroutine;
    private void Awake()
    {
        Init<QuickFinger>(this);
        parameters = new Parameters(startDuration);
        base.Awake();
    }

    protected override void OnUpgrade()
    {
        parameters.Duration = startDuration + increaseDuration * (level - 1);
    }

    protected override Ball ConfigureBall(Ball ball)
    {
        ball.onDestroy.AddListener(() =>
        {
            ActivateForSeconds(parameters.Duration);
        });
        
        return ball;
    }

    private void ActivateForSeconds(float seconds)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(ActivateRoutine(seconds));
    }

    private IEnumerator ActivateRoutine(float seconds)
    {
        IsActive = true;
        view.ShowForSeconds(seconds);
        yield return new WaitForSeconds(seconds);
        IsActive = false;
    }

    public class Parameters
    {
        public float Duration {get; internal set;}

        public Parameters(float duration)
        {
            this.Duration = duration;
        }
    }
}
