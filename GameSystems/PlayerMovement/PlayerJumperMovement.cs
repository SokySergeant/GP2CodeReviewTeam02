using FMODUnity;
using GameSystems.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystems.PlayerMovement
{
    public class PlayerJumperMovement : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private FloatVariable jumpPower;

        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;

        [Header("Studio Event Emitter References")]
        [SerializeField] private StudioEventEmitter jumpEmitter;
        


        public void OnJump(InputAction.CallbackContext ctx)
        {
            if(!playerMovement.currentCanMove || !ctx.started) return;
            
            if(playerMovement.isGrounded)
            {
                playerMovement.velocity.y = jumpPower.Value;
                jumpEmitter.Play();
            }
        }
        
        
        
    }
}