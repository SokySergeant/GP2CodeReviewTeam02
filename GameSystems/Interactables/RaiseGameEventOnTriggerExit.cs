using GameSystems.GameEventLogic;
using UnityEngine;

public class RaiseGameEventOnTriggerExit : MonoBehaviour
{
    [SerializeField] private LayerMask whatCanTrigger;
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private bool repeatable = false;
    private bool _canTrigger = true;


    
    private void OnTriggerExit(Collider other)
    {
        if(!_canTrigger) return;
        
        if((whatCanTrigger & (1 << other.gameObject.layer)) != 0)
        {
            gameEvent.Raise();

            if(repeatable) return;
            _canTrigger = false;
        }
    }
}
