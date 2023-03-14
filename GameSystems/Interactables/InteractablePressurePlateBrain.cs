using UnityEngine;

public class InteractablePressurePlateBrain : Interactable
{
    [Header("Pressure Plate Variables")]
    [SerializeField] private LayerMask whatCanPress;
    private InteractablePressurePlate[] _pressurePlates;



    private void FixedUpdate()
    {
        if(!_canInteract) return;

        SetPressurePlatesColor();
    }



    private void SetPressurePlatesColor()
    {
        for(int i = 0; i < _pressurePlates.Length; i++)
        {
            if(_pressurePlates[i].isPressed)
            {
                _lights[i].color = completedColor;
            } else
            {
                _lights[i].color = enabledColor;
            }
        }
    }
    


    protected override void OnEnableForInheriting()
    {
        _pressurePlates = GetComponentsInChildren<InteractablePressurePlate>();

        for(int i = 0; i < _pressurePlates.Length; i++)
        {
            _pressurePlates[i].whatCanPress = whatCanPress;
            _pressurePlates[i].onPress += CheckIfAllPressurePlatesPressed;
        }
    }

    

    protected override void OnDisableForInheriting()
    {
        for(int i = 0; i < _pressurePlates.Length; i++)
        {
            _pressurePlates[i].onPress -= CheckIfAllPressurePlatesPressed;
        }
    }


    
    private void CheckIfAllPressurePlatesPressed()
    {
        if(AreAllPressurePlatesPressed())
        {
            if(!repeatable)
            {
                for(int i = 0; i < _pressurePlates.Length; i++)
                {
                    _pressurePlates[i].canPress = false;
                }
            }
            CompletedInteraction();
        }
    }



    public bool AreAllPressurePlatesPressed()
    {
        for(int i = 0; i < _pressurePlates.Length; i++)
        {
            if(!_pressurePlates[i].isPressed) return false;
        }

        return true;
    }
}
