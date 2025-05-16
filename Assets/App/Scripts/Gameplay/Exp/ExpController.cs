using UnityEngine;

public class ExpController : MonoBehaviour, IBallDestroyModifactor
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Exp expPrefab;
    [SerializeField] private ProgressController progressController;
    
    public void CreateExp(Vector3 position)
    {
        Vector3[] corners = new Vector3[4];  
        target.GetWorldCorners(corners);

        float minY = corners[0].y, minX = corners[0].x, maxX = corners[3].x;
        var exp = Instantiate(expPrefab, position, Quaternion.identity, transform);
        Vector3 targetPos = new Vector3(Random.Range(minX, maxX), minY);
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
        ball.onDestroy.AddListener(()  => CreateExp(ball.transform.position));
    }
}
