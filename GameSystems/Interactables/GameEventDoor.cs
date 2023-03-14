using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDoor : MonoBehaviour
{
    [SerializeField] private DoorDir doorDir;
    [SerializeField] private float distanceToOpen;
    
    private Vector3 _currentDir;
    private float _baseY;
    private float _currentDistance;

    private bool _isOpen = false;



    private void OnEnable()
    {
        switch(doorDir)
        {
            case DoorDir.Up:
                _currentDir = Vector3.up;
                break;
            case DoorDir.Down:
                _currentDir = Vector3.down;
                break;
        }
    }



    public void OpenDoor()
    {
        if(_isOpen) return;
        _isOpen = true;
        StartCoroutine(OpenDoorRoutine());
    }
    
    
    
    public void CloseDoor()
    {
        if(!_isOpen) return;
        _isOpen = false;
        StartCoroutine(CloseDoorRoutine());
    }

    

    private IEnumerator OpenDoorRoutine()
    {
        _baseY = transform.position.y;

        while(_currentDistance < distanceToOpen)
        {
            float distanceThisStep = distanceToOpen * Time.fixedDeltaTime;
            transform.Translate(_currentDir * distanceThisStep);
            _currentDistance += distanceThisStep;

            yield return new WaitForFixedUpdate();
        }
    }




    private IEnumerator CloseDoorRoutine()
    {
        while(_currentDistance > 0)
        {
            float distanceThisStep = distanceToOpen * Time.fixedDeltaTime;
            transform.Translate(-_currentDir * distanceThisStep);
            _currentDistance -= distanceThisStep;
                
            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(transform.position.x, _baseY, transform.position.z);
    }
    


}
