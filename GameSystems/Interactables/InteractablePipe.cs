using System.Collections;
using FMODUnity;
using GameSystems.AgentLogic;
using GameSystems.PlayerMovement;
using UnityEngine;

public enum SpinAxis
{
    X,
    Y,
    Z
}

public enum SpinDir
{
    Clockwise,
    Counterclockwise
}

public class InteractablePipe : Interactable
{
    [SerializeField] private SpinAxis spinAxis;
    [SerializeField] private SpinDir spinDir;
    [SerializeField] private StudioEventEmitter pipeSpinEmitter;
    private bool _canSpin = true;

    private Quaternion _defaultRot;
    
    private GameObject _crawler;

    

    protected override void OnEnableForInheriting()
    {
        _defaultRot = transform.rotation;
    }

    

    protected override void TurnOnForInheriting()
    {
        if(!_canSpin) return;
        StartCoroutine(Spin());
    }



    private IEnumerator Spin()
    {
        _canSpin = false;

        Quaternion targetRot = transform.rotation;
        float angle = 90;
        
        switch(spinDir)
        {
            case SpinDir.Clockwise:
                angle = 90;
                break;
            case SpinDir.Counterclockwise:
                angle = -90;
                break;
        }

        switch(spinAxis)
        {
            case SpinAxis.X:
                targetRot = Quaternion.Euler(angle, 0, 0) * targetRot;
                break;
            case SpinAxis.Y:
                targetRot = Quaternion.Euler(0, angle, 0) * targetRot;
                break;
            case SpinAxis.Z:
                targetRot = Quaternion.Euler(0, 0, angle) * targetRot;
                break;
        }
        
        FreezeCrawler();

        pipeSpinEmitter.Play();
        
        while(Quaternion.Angle(transform.rotation, targetRot) > 5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.2f);
            yield return new WaitForFixedUpdate();
        }
        
        transform.rotation = targetRot;
        
        UnfreezeCrawler();
        
        _canSpin = true;
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            if(other.gameObject.GetComponent<PlayerCrawlerMovement>() != null)
            {
                _crawler = other.gameObject;
                _crawler.gameObject.GetComponent<PlayerManager>().pipe = this;
            }
        }
    }
    
    
    
    private void OnTriggerExit(Collider other)
    {
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            if(_crawler == other.gameObject)
            {
                UnfreezeCrawler();
                ReleaseCrawler();
            }
        }
    }



    private void FreezeCrawler()
    {
        if(_crawler != null)
        {
            _crawler.GetComponent<PlayerMovement>().ToggleIsControllable(false);
            _crawler.GetComponent<CharacterController>().enabled = false;
            _crawler.transform.SetParent(transform); 
        }
    }



    private void UnfreezeCrawler()
    {
        if(_crawler != null)
        {
            _crawler.GetComponent<PlayerMovement>().ToggleIsControllable(true);
            _crawler.GetComponent<CharacterController>().enabled = true;
            _crawler.transform.SetParent(null);
        }
    }



    public void ReleaseCrawler()
    {
        _crawler = null;
    }


    
    protected override void ResetSelfForInheriting()
    {
        transform.rotation = _defaultRot;
    }
}
