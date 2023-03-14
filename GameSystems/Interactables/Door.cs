using System.Collections;
using FMODUnity;
using GameSystems.GameEventLogic;
using UnityEngine;

public enum DoorDir
{
    Up,
    Down
}

public class Door : Interactable
{
    [Header("Door stuff")]
    [SerializeField] private DoorDir doorDir;
    [SerializeField] private GameEvent[] GameEventsOnOpen;
    [SerializeField] private float distanceToOpen;
    [SerializeField] private float timeToStayOpen;
    [SerializeField] private bool isBigDoor = true;
    [SerializeField] private StudioEventEmitter bigDoorOpenEmitter;
    [SerializeField] private StudioEventEmitter smallDoorOpenEmitter;
    private Vector3 _currentDir;
    
    private float _baseY;
    private float _currentDistance;
    private bool _canOpen = true;

    

    protected override void TurnOnForInheriting()
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
        
        if(!_canOpen) return;
        StartCoroutine(OpenDoorRoutine());
    }

    

    private IEnumerator OpenDoorRoutine()
    {
        _canOpen = false;

        _baseY = transform.position.y;

        if(isBigDoor)
        {
            bigDoorOpenEmitter.Play();
        } else
        {
            smallDoorOpenEmitter.Play();
        }

        while(_currentDistance < distanceToOpen)
        {
            float distanceThisStep = distanceToOpen * Time.fixedDeltaTime;
            transform.Translate(_currentDir * distanceThisStep);
            _currentDistance += distanceThisStep;

            yield return new WaitForFixedUpdate();
        }
        
        if(_locks > 0)
        {
            for(int i = 0; i < myInteractables.Count; i++)
            {
                InteractablePressurePlateBrain tempPressurePlateBrain = myInteractables[i] as InteractablePressurePlateBrain;
                if(tempPressurePlateBrain != null)
                {
                    yield return new WaitWhile(() => tempPressurePlateBrain.AreAllPressurePlatesPressed());
                }
            }

           
            yield return new WaitForSeconds(timeToStayOpen);
            
            while(_currentDistance > 0)
            {
                float distanceThisStep = distanceToOpen * Time.fixedDeltaTime;
                transform.Translate(-_currentDir * distanceThisStep);
                _currentDistance -= distanceThisStep;
                
                yield return new WaitForFixedUpdate();
            }

            transform.position = new Vector3(transform.position.x, _baseY, transform.position.z);
            _canOpen = true;
        }
        if (GameEventsOnOpen.Length > 0) {
            for (int i = 0; i < GameEventsOnOpen.Length; i++) {
                GameEventsOnOpen[i].Raise();
            }
        }
    }
    


    
}
