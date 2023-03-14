using System.Collections;
using System.Collections.Generic;
using GameSystems.GameEventLogic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private GameEvent fadeOutLoading;
    [SerializeField] private GameEvent fadeInControls;
    [SerializeField] private GameEvent fadeOutControls;
    private bool _canTrigger = true;
    private bool _canFadeInControls = true;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(!_canTrigger) return;
        
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            StartCoroutine(FadeGameInAndShowControls());
        }
    }
    
    
    
    private void OnTriggerExit(Collider other)
    {
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            _canFadeInControls = false;
            fadeOutControls.Raise();
        }
    }
    
    
    
    private IEnumerator FadeGameInAndShowControls()
    {
        _canTrigger = false;
        
        yield return new WaitForSeconds(1);
        
        fadeOutLoading.Raise();

        yield return new WaitForSeconds(1);

        if(_canFadeInControls)
        {
            fadeInControls.Raise();
        }
    }
}
