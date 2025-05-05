using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ObjectAboveIndicator : MonoBehaviour
{
    [FormerlySerializedAs("OnGamefieldNotFull")] [HideInInspector] public UnityEvent OnNoObjectsAbove = new(); 

    float height;

    private void Awake()
    {
        height = transform.position.y;
    }

    private void Update()
    {
        if(!PhysicsController.CheckBodiesAboveHorizontalLine(height))
        {
            OnNoObjectsAbove.Invoke();
        }
    }
}
