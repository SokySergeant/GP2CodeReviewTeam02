using GameSystems.Interactables.EffectStuff;
using GameSystems.PlayerMovement;
using UnityEngine;

public enum BeltDir
{
    Forward,
    Backward
}

public class ConveyorBelt : EffectAbstract
{
    public BeltDir beltDir;
    [SerializeField] private float beltSpeed;
    
    private Vector3 _pushDir; 
    


    private void Start()
    {
        SetPushDir(beltDir);
    }



    public void SetPushDir(BeltDir givenBeltDir)
    {
        beltDir = givenBeltDir;
        _pushDir = GetPushDir(givenBeltDir);
    }
    
    
    
    private Vector3 GetPushDir(BeltDir givenBeltDir)
    {
        switch(givenBeltDir)
        {
            case BeltDir.Forward:
                return transform.forward;
                break;
            case BeltDir.Backward:
                return -transform.forward;
                break;
            default:
                return Vector3.zero;
        }
    }

    

    protected override void OnPlayerStay(GameObject player)
    {
        player.GetComponent<CharacterController>().Move(_pushDir * beltSpeed * Time.fixedDeltaTime);
    }

    protected override void OnPlayerExit(GameObject player)
    {
        player.GetComponent<PlayerMovement>().impact += _pushDir * beltSpeed;
    }

    protected override void OnItemStay(GameObject item)
    {
        item.transform.position += _pushDir * beltSpeed * Time.fixedDeltaTime;
    }
    
    
    
}
