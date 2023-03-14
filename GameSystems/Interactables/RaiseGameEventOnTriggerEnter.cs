using System.Collections;
using GameSystems.GameEventLogic;
using UnityEngine;

public class RaiseGameEventOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private LayerMask whatCanTrigger;
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private bool repeatable = false;
    [SerializeField] private float secondsToWaitOnStartup = 0;
    private bool _canTrigger = true;
    private bool _isBusy = false;



    private void OnTriggerEnter(Collider other)
    {
        if(!_canTrigger || _isBusy) return;
        
        if((whatCanTrigger & (1 << other.gameObject.layer)) != 0)
        {
            StartCoroutine(DoTrigger());
        }
    }



    private IEnumerator DoTrigger()
    {
        _isBusy = true;
        
        yield return new WaitForSeconds(secondsToWaitOnStartup);

        gameEvent.Raise();

        if(!repeatable)
        {
            _canTrigger = false;
        }

        _isBusy = false;
    }
}
