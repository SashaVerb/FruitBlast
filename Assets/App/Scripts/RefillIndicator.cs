using UnityEngine;
using UnityEngine.Events;

public class RefillIndicator : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnGamefieldEmpty = new(); 

    float height;

    private void Awake()
    {
        height = transform.position.y;
    }

    private void Update()
    {
        if(!PhysicsController.CheckBodiesAboveHorizontalLine(height))
        {
            OnGamefieldEmpty.Invoke();
        }
    }
}
