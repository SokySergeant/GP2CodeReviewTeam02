using FMODUnity;
using UnityEngine;

public class InteractablePressurePlate : MonoBehaviour
{
    [HideInInspector] public LayerMask whatCanPress;
    [HideInInspector] public bool isPressed;
    [SerializeField] private StudioEventEmitter pressurePlateEnterEmitter;
    [SerializeField] private StudioEventEmitter pressurePlateExitEmitter;
    
    public delegate void OnPress();
    public event OnPress onPress;

    [HideInInspector] public bool canPress = true;
    

    
    private void OnTriggerEnter(Collider other)
    {
        if(!canPress) return;
        
        if((whatCanPress & (1 << other.gameObject.layer)) != 0)
        {
            pressurePlateEnterEmitter.Play();
            onPress?.Invoke();
        }
    }

    

    private void OnTriggerStay(Collider other)
    {
        if((whatCanPress & (1 << other.gameObject.layer)) != 0)
        {
            isPressed = true;
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if(!canPress) return;
        
        if((whatCanPress & (1 << other.gameObject.layer)) != 0)
        {
            pressurePlateExitEmitter.Play();
            isPressed = false;
        }
    }
}
