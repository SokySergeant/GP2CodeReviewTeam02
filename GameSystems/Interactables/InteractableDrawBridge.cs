using System.Collections;
using FMODUnity;
using UnityEngine;

public class InteractableDrawBridge : Interactable
{
    [Header("Draw Bridge Variables")]
    [SerializeField] private float startAngle;
    [SerializeField] private float endAngle;
    [SerializeField] private Transform bridge;
    [SerializeField] private StudioEventEmitter bridgeUp;
    [SerializeField] private StudioEventEmitter bridgeDown;

    private Quaternion _startRot;
    private Quaternion _endRot;
    private bool _isLowered = false;

    private bool _canMoveBridge = true;


    
    protected override void OnEnableForInheriting()
    {
        _startRot = Quaternion.Euler(bridge.rotation.eulerAngles.x, bridge.rotation.eulerAngles.y, startAngle);
        _endRot = Quaternion.Euler(bridge.rotation.eulerAngles.x, bridge.rotation.eulerAngles.y, endAngle);
        bridge.rotation = _startRot;
    }

    

    protected override void TurnOnForInheriting()
    {
        if(!_canMoveBridge) return;
        StartCoroutine(MoveBridge());
    }



    private IEnumerator MoveBridge()
    {
        _canMoveBridge = false; 
        
        Quaternion targetRot;
        
        if(_isLowered)
        {
            bridgeUp.Play();
            targetRot = _startRot;
        } else
        {
            bridgeDown.Play();
            targetRot = _endRot;
        }

        while(Quaternion.Angle(bridge.rotation, targetRot) > 1f)
        {
            bridge.rotation = Quaternion.Slerp(bridge.rotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        
        bridge.rotation = targetRot;

        _isLowered = !_isLowered;

        _canMoveBridge = true;
    }
}
