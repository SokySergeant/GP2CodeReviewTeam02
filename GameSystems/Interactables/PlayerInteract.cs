using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public delegate void OnPlayerInteract();
    public event OnPlayerInteract onPlayerInteract;
    
    public delegate void OnPlayerEndInteract();
    public event OnPlayerEndInteract onPlayerEndInteract;



    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            onPlayerInteract?.Invoke();
        }

        if(ctx.canceled)
        {
            onPlayerEndInteract?.Invoke();
        }
    }
}
