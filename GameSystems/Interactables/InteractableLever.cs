using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

public class InteractableLever : Interactable
{
    [Header("Lever stuff")]
    [SerializeField] private Transform lever;
    [SerializeField] private float angle;
    [SerializeField] private StudioEventEmitter leverEmitter;
    [HideInInspector] public bool isPulled = false;
    private bool _canPull = false;


    protected override void OnEnableForInheriting()
    {
        if(myInteractables.Count == 0)
        {
            _canPull = true;
        }
    }

    

    protected override void TurnOnForInheriting()
    {
        _canPull = true;
    }


    
    protected override void Interacted()
    {
        if(!_canPull) return;
        
        isPulled = !isPulled;
        StartCoroutine(MoveLever());

        CompletedInteraction();
    }



    private IEnumerator MoveLever()
    {
        _canPull = false;
        
        int sign = Convert.ToInt32(!isPulled) * 2 - 1;
        Quaternion targetRot = lever.rotation;
        targetRot = Quaternion.Euler(0, 0, angle * sign) * targetRot;

        leverEmitter.Play();

        while(Quaternion.Angle(lever.rotation, targetRot) > 2f)
        {
            lever.rotation = Quaternion.Slerp(lever.rotation, targetRot, 0.3f);
            yield return new WaitForFixedUpdate();
        }
        
        lever.rotation = targetRot;
        
        for(int i = 0; i < _lights.Length; i++)
        {
            if(isPulled)
            {
                _lights[i].color = completedColor;
            } else
            {
                _lights[i].color = enabledColor;
            }
        }
        
        _canPull = true;
    }
}
