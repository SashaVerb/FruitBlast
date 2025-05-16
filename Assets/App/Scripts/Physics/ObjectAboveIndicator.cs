using UnityEngine;
using UnityEngine.Events;

public class ObjectAboveIndicator : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnNoObjectsAbove = new(); 

    private float height;
    private bool hasObjects;
    
    public bool HasObjects => hasObjects;
    private void Awake()
    {
        height = transform.position.y;
    }

    private void Update()
    {
        if(!PhysicsController.CheckBodiesAboveHorizontalLine(height))
        {
            hasObjects = false;
            OnNoObjectsAbove.Invoke();
        }
        else
            hasObjects = true;
    }
}
