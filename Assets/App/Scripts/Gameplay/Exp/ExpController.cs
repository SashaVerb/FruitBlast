using UnityEngine;

public class ExpController : MonoBehaviour, IBallDestroyModifactor
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Exp expPrefab;
    [SerializeField] private ProgressController progressController;
    [SerializeField] private CircleScaler circleScaler;
    
    public void CreateExp(Vector3 position)
    {
        Vector3[] corners = new Vector3[4]; 
        target.GetWorldCorners(corners);

        float centerY = (corners[0].y + corners[1].y) * 0.5f, minX = corners[0].x, maxX = corners[3].x;
        var exp = Instantiate(expPrefab, position, Quaternion.identity, transform);
        circleScaler.AdjustObjectScale(exp.transform);
        Vector3 targetPos = new Vector3(Random.Range(minX, maxX), centerY);
        exp.SetTarget(targetPos);
        exp.OnDestroyMoment.AddListener(() =>
        {
            var luckyGuy = PerkSystem.GetPerk<LuckyGuy>();
            if (luckyGuy != null && luckyGuy.Triggered())
                progressController.AddProgress((int)(exp.Cost * luckyGuy.Multiplier));
            else 
                progressController.AddProgress(exp.Cost);
        });
    }

    public void Modificate(Ball ball)
    {
        ball.onDestroy.AddListener(() => CreateExp(ball.transform.position));
    }
}
