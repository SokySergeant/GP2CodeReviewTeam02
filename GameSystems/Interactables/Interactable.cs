using System.Collections;
using System.Collections.Generic;
using GameSystems.GameEventLogic;
using GameSystems.PlayerMovement;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("References")]
    private Canvas _rootInteractableCanvas;
    [SerializeField] private GameObject uiPrefab;
    protected GameObject _tempUiPrefab;

    private Coroutine _moveUiPrefab;
    
    [SerializeField] protected LayerMask whatIsPlayer;
    private PlayerInteract _player;

    [Header("Game Events Called On Completed")]
    [SerializeField] private GameFloatEvent gameFloatEvent;
    
    [Header("Generic Variables")]
    [SerializeField] protected List<Interactable> myInteractables = new List<Interactable>();
    public bool repeatable = false;
    [SerializeField] private float cooldownTime;

    [SerializeField] protected Color enabledColor;
    [SerializeField] protected Color disabledColor;
    [SerializeField] protected Color completedColor;
    protected Light[] _lights;
    
    protected int _locks = 0;
    private bool _isInCooldown;
    protected bool _canInteract;

    public delegate void OnCompletedInteraction();
    public event OnCompletedInteraction onCompletedInteraction;
    
    [SerializeField] private bool canJumperInteract = true;
    [SerializeField] private bool canCrawlerInteract = true;
    
    
    
    private void Start()
    {
        Setup();
    }



    private void Setup()
    {
        _rootInteractableCanvas = GameObject.Find("RootInteractableCanvas").GetComponent<Canvas>();
        _isInCooldown = false;
        _canInteract = false;
        
        _lights = GetComponentsInChildren<Light>();
        
        _locks = myInteractables.Count;
        if(_locks <= 0)
        {
            _canInteract = true;
            SetLightsEnabled();
        } else
        {
            SetLightsDisabled();
        }
    }
    
    
    
    private void OnEnable()
    {
        for(int i = 0; i < myInteractables.Count; i++)
        {
            myInteractables[i].onCompletedInteraction += OpenOneLock;
        }
        
        OnEnableForInheriting();
    }
    
    
    
    private void OnDisable()
    {
        for(int i = 0; i < myInteractables.Count; i++)
        {
            myInteractables[i].onCompletedInteraction -= OpenOneLock;
        }
        
        OnDisableForInheriting();
    }
    
    
    
    private void OpenOneLock()
    {
        _locks--;

        if(_locks <= 0)
        {
            _locks = 0;
            _canInteract = true;
            SetLightsEnabled();
            TurnOnForInheriting();
            
            for(int i = 0; i < myInteractables.Count; i++)
            {
                if(myInteractables[i].repeatable)
                {
                    _locks++;
                }
            }
        }
    }
    
    
    
    protected virtual void OnEnableForInheriting() {}
    protected virtual void OnDisableForInheriting() {}
    protected virtual void TurnOnForInheriting() {}

    private void SetLightsDisabled()
    {
        for(int i = 0; i < _lights.Length; i++)
        {
            _lights[i].color = disabledColor;
        }
    }

    private void SetLightsEnabled()
    {
        for(int i = 0; i < _lights.Length; i++)
        {
            _lights[i].color = enabledColor;
        }
    }

    private void SetLightsCompleted()
    {
        for(int i = 0; i < _lights.Length; i++)
        {
            _lights[i].color = completedColor;
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(!_canInteract) return;
        
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            var tempCrawler = other.GetComponent<PlayerCrawlerMovement>();
            var tempJumper = other.GetComponent<PlayerJumperMovement>();
            
            if((tempCrawler != null && canCrawlerInteract) || (tempJumper != null && canJumperInteract))
            {
                var tempPlayer = other.GetComponent<PlayerInteract>();

                if(_player != null) return;
                _player = tempPlayer;

                if(!_isInCooldown)
                {
                    StartInteraction();
                }
            }
        }
    }
    


    private void OnTriggerExit(Collider other)
    {
        if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
        {
            var tempPlayer = other.GetComponent<PlayerInteract>();
            
            if(_player != tempPlayer) return;
            EndInteraction();
            
            _player = null;
        }
    }



    private void StartInteraction()
    {
        _player.onPlayerInteract += Interacted;
        
        _player.onPlayerInteract += ScaleDownUI;
        _player.onPlayerEndInteract += ScaleUpUI;
            
        ShowUI();
        OnPlayerEnter();
    }



    private void EndInteraction()
    {
        _player.onPlayerInteract -= Interacted;
        
        _player.onPlayerInteract -= ScaleDownUI;
        _player.onPlayerEndInteract -= ScaleUpUI;

        RemoveUI();
        OnPlayerExit();
    }



    private void ShowUI()
    {
        if(uiPrefab == null) return;
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        _tempUiPrefab = Instantiate(uiPrefab, new Vector3(screenPos.x, screenPos.y, 0), Quaternion.identity);
        _tempUiPrefab.transform.SetParent(_rootInteractableCanvas.transform, false);

        _moveUiPrefab = StartCoroutine(MoveUiPrefab());
    }


    
    private void RemoveUI()
    {
        if(uiPrefab == null) return;
        
        StopCoroutine(_moveUiPrefab);
        if(_tempUiPrefab == null) return;
        Destroy(_tempUiPrefab);
    }
    
    

    private IEnumerator MoveUiPrefab()
    {
        while(true)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            _tempUiPrefab.transform.position = new Vector3(screenPos.x, screenPos.y, 0);

            yield return new WaitForFixedUpdate();
        }
    }



    private void ScaleDownUI()
    {
        if(_tempUiPrefab == null) return;

        _tempUiPrefab.transform.localScale = new Vector3(0.8f, 0.8f, 1);
    }



    private void ScaleUpUI()
    {
        if(_tempUiPrefab == null) return;

        _tempUiPrefab.transform.localScale = new Vector3(1, 1, 1);
    }



    protected void StartCooldown()
    {
        if(_isInCooldown) return;
        StartCoroutine(Cooldown());
    }
    
    private IEnumerator Cooldown()
    {
        _isInCooldown = true;
        
        EndInteraction();
        
        yield return new WaitForSeconds(cooldownTime);

        if(_player != null)
        {
            StartInteraction();
        }

        _isInCooldown = false;
    }
    
    
    
    protected virtual void OnPlayerEnter() {}
    protected virtual void OnPlayerExit() {}
    protected virtual void Interacted() {}



    protected void CompletedInteraction()
    {
        if(!_canInteract) return;
        
        onCompletedInteraction?.Invoke();

        if(gameFloatEvent != null)
        {
            gameFloatEvent.Raise(0);
        }

        if(!repeatable)
        {
            RemoveUI();
            SetLightsCompleted();
            _canInteract = false;
        }
    }



    public void ResetSelf()
    {
        Setup();
        ResetSelfForInheriting();
    }

    protected virtual void ResetSelfForInheriting() {}
}
