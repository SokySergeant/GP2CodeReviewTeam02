using System.Collections;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemMover : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private LayerMask whatIsItem;
    [SerializeField] private Transform target;
    [SerializeField] private Transform arm;
    [SerializeField] private float armSpeed;
    [SerializeField] private float aboveItemOffset = 0.5f;
    [SerializeField] private float aboveTargetOffset = 4f;

    [Header("Studio Emitters")]
    [SerializeField] private StudioEventEmitter clawMove;

    private Vector3 _defaultArmPos;
    private bool _isBusy = false;
    private float _tolerance = 1f;
    
    
    
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        _defaultArmPos = arm.position;
    }

    

    private void OnTriggerStay(Collider other)
    {
        if(_isBusy) return;
        
        if((whatIsItem & (1 << other.gameObject.layer)) != 0)
        {
            StartCoroutine(MoveItem(other.transform));
        }
    }



    private IEnumerator MoveItem(Transform item)
    {
        _isBusy = true;
        clawMove.Play();

        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        //bring arm to box
        yield return StartCoroutine(MoveArmTo(item, aboveItemOffset));
        
        //grab box
        itemRb.isKinematic = true;
        item.SetParent(arm);
        
        //move arm above target
        yield return StartCoroutine(MoveArmTo(target, aboveTargetOffset));
        
        //release box
        itemRb.isKinematic = false;
        item.SetParent(null);
        
        //bring arm back
        yield return StartCoroutine(MoveArmTo(_defaultArmPos));

        clawMove.Stop();
        _isBusy = false;
    }



    private IEnumerator MoveArmTo(Vector3 pos)
    {
        while(Vector3.Distance(arm.position, pos) > _tolerance)
        {
            arm.position += (pos - arm.position).normalized * (armSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    
    
    
    private IEnumerator MoveArmTo(Transform trans, float offset)
    {
        Vector3 pos;
        do 
        {
            pos = trans.position + (Vector3.up * offset);
            arm.position += (pos - arm.position).normalized * (armSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        } while(Vector3.Distance(arm.position, pos) > _tolerance);
    }
}
