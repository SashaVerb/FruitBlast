using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private PhysicsController physicsController;
    [SerializeField] private BallFactory ballFactory;
    [SerializeField] private CircleScaler circleScaler;
    [SerializeField] private Transform gameField;

    private void Start()
    {
        FillGameField();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PopBall();
        }
    }

    private void PopBall()
    {
        var body = PhysicsController.GetBodyAt(camera.ScreenToWorldPoint(Input.mousePosition));
        if (body != null)
        {
            body.GetComponent<Ball>().Pop();
        }
    }

    private Ball CreateConfiguredBall()
    {
        var newBall = ballFactory.Create();
        float randomX = Random.Range(gameField.position.x - gameField.localScale.x * 0.5f, gameField.position.x + gameField.localScale.x * 0.5f),
            randomY = Random.Range(gameField.position.y + gameField.localScale.y * 0.5f, gameField.position.y + gameField.localScale.y * 0.5f + 3f);
        newBall.transform.position = new Vector3(randomX, randomY, 0);

        physicsController.AddPhysicBody(newBall.physicBody);

        return newBall;
    }

    public void PlusOneBall()
    {
        CreateConfiguredBall();
    }

    public void FillGameField()
    {
        StartCoroutine(FillGameFieldRoutine());
    }

    private IEnumerator FillGameFieldRoutine()
    {
        float area = gameField.localScale.x * gameField.localScale.y, radius;
        while (area > 0)
        {
            radius = CreateConfiguredBall().physicBody.Radius;
            area -= radius * radius * 4f;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
