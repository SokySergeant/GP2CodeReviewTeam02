using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpawnedItem : MonoBehaviour
{
    [SerializeField] private LayerMask whatCanDestroy;

    public delegate void OnReset(GameObject self);
    public OnReset onReset;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if((whatCanDestroy & (1 << other.gameObject.layer)) != 0)
        {
            onReset?.Invoke(gameObject);
        }
    }
}
