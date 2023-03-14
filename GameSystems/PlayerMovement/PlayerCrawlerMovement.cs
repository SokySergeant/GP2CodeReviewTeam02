using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystems.PlayerMovement
{
    public class PlayerCrawlerMovement : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private LayerMask whatIsPipe; 

        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private StudioEventEmitter crawlingInPipeEmitter;

        private Coroutine _moveUpRoutine;
        private Coroutine _moveDownRoutine;
        
        [HideInInspector] public bool isInPipe = false;

        
        
        private void Update()
        {
            SwitchMovementIfInPipe();
        }



        private void SwitchMovementIfInPipe()
        {
            bool tempIsInPipe = Physics.CheckSphere(transform.position, 0.5f, whatIsPipe);
            if(isInPipe != tempIsInPipe)
            {
                isInPipe = tempIsInPipe;
                if(isInPipe)
                {
                    playerMovement.currentGravity = 0;
                    playerMovement.velocity.y = 0;
                } else
                {
                    playerMovement.currentGravity = playerMovement.gravity.Value;
                    playerMovement.velocity.y = 0;
                    crawlingInPipeEmitter.Stop();
                    if(_moveUpRoutine != null)
                    {
                        StopCoroutine(_moveUpRoutine);
                    }
                    if(_moveDownRoutine != null)
                    {
                        StopCoroutine(_moveDownRoutine);
                    }
                }
            }
        }
        


        public void OnJump(InputAction.CallbackContext ctx)
        {
            if(!playerMovement.currentCanMove) return;

            if(ctx.started && isInPipe)
            {
                crawlingInPipeEmitter.Play();
                _moveUpRoutine = StartCoroutine(MoveUp());
            }

            if(ctx.canceled)
            {
                if(_moveUpRoutine != null)
                {
                    crawlingInPipeEmitter.Stop();
                    StopCoroutine(_moveUpRoutine);
                }
            }
        }



        public void OnDownwards(InputAction.CallbackContext ctx)
        {
            if(!playerMovement.currentCanMove) return;

            if(ctx.started && isInPipe)
            {
                crawlingInPipeEmitter.Play();
                _moveDownRoutine = StartCoroutine(MoveDown());
            }

            if(ctx.canceled)
            {
                if(_moveDownRoutine != null)
                {
                    crawlingInPipeEmitter.Stop();
                    StopCoroutine(_moveDownRoutine);
                }
            }
        }



        private IEnumerator MoveUp()
        {
            while(true)
            {
                if(playerMovement.controller.enabled)
                {
                    playerMovement.controller.Move((playerMovement.currentPlayerSpeed * Time.fixedDeltaTime) * Vector3.up);
                }
                yield return new WaitForFixedUpdate();
            }
        }

        
        
        private IEnumerator MoveDown()
        {
            while(true)
            {
                if(playerMovement.controller.enabled)
                {
                    playerMovement.controller.Move((playerMovement.currentPlayerSpeed * Time.fixedDeltaTime) * Vector3.down);
                }
                yield return new WaitForFixedUpdate();
            }
        }
        
        
    }
}