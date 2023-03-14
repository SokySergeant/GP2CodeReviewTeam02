using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class InteractableButtonMash : Interactable
{
    [Header("Button Mash Variables")]
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private float mashPower;
    [SerializeField] private StudioEventEmitter buttonMashLoopEmitter;
    private Slider _slider;
    private bool _doButtonMash = false;



    private void FixedUpdate()
    {
        if(!_doButtonMash) return;

        _slider.value -= decreaseSpeed * Time.fixedDeltaTime;
        _slider.value = Mathf.Clamp(_slider.value, 0, 1);
    }

    

    protected override void Interacted()
    {
        if(!_canInteract) return;
        
        _slider.value += mashPower * Time.fixedDeltaTime;
        if(_slider.value >= 1)
        {
            if(buttonMashLoopEmitter != null)
            {
                buttonMashLoopEmitter.Stop();
            }
            CompletedInteraction();
        }
    }

    

    protected override void OnPlayerEnter()
    {
        _slider = _tempUiPrefab.GetComponentInChildren<Slider>();
        _slider.value = 0;
        _doButtonMash = true;

        if(buttonMashLoopEmitter != null)
        {
            buttonMashLoopEmitter.Play();
        }
    }

    
    
    protected override void OnPlayerExit()
    {
        if(buttonMashLoopEmitter != null)
        {
            buttonMashLoopEmitter.Stop();
        }
    }
}
