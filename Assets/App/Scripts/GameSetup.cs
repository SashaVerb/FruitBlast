using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private void Awake()
    {
        SetupRandom();
    }

    private void SetupRandom()
    {
        int seed = Random.Range(1000, 9999);
        Random.InitState(6131);
        Debug.Log("Seed: " + seed);
    }
}
