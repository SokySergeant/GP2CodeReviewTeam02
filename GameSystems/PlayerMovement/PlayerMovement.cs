using FMODUnity;
using GameSystems.GameEventLogic;
using GameSystems.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystems.PlayerMovement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Variables")]
        public FloatVariable playerSpeed;
        [SerializeField] private FloatVariable playerMoveDamp;

        public FloatVariable gravity;
        public BoolVariable canMove;

        [SerializeField] private LayerMask whatIsGround;

        [Header("References")]
        public CharacterController controller;
        public Animator animator;

        [SerializeField] private Transform playerObjTransform;
        [SerializeField] private Transform groundCheck;

        [Header("Studio Event Emitter References")]
        [SerializeField] private StudioEventEmitter walkEmitter;

        private Vector3 _move = Vector3.zero;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private Vector3 _playerLook;

        [HideInInspector] public float currentPlayerSpeed;
        [HideInInspector] public bool currentCanMove;

        private float _groundRadius = 0.5f;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public float currentGravity;

        [HideInInspector] public Vector3 velocity;
        [HideInInspector] public Vector3 impact;

        private bool _canDash = true;
        private float _dashTime;
        private float _currentDashTime;
        


        private void Start()
        {
            SetDefaultValues();
        }

        

        private void FixedUpdate()
        {
            SetIsGrounded();
            ApplyGravity();
            ApplyForce();
            MovePlayer();
            RotatePlayerObj();
            SetAnimatorMove();
            SetWalkingSound();
        }
        
        

        private void SetDefaultValues()
        {
            Cursor.lockState = CursorLockMode.Locked;
            currentGravity = gravity.Value;

            currentPlayerSpeed = playerSpeed.Value;
            currentCanMove = canMove.Value;
        }



        private void SetIsGrounded()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, _groundRadius, whatIsGround);
        }

        
        
        private void ApplyGravity()
        {
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += currentGravity * Time.fixedDeltaTime;
            if(controller.enabled)
            {
                controller.Move(velocity * Time.fixedDeltaTime);
            }
        }
        
        
        
        private void ApplyForce()
        {
            if(impact.magnitude < 0.1f) return;

            controller.Move(impact * Time.fixedDeltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, Time.fixedDeltaTime);
            
            if(isGrounded)
            {
                impact = Vector3.Lerp(impact, Vector3.zero, 2 * Time.fixedDeltaTime);
            }
        }
        
        
        
        private void MovePlayer()
        {
            if(!currentCanMove) return;
            
            _move = Vector3.Lerp(_move, (Vector3.right * _moveInput.x + Vector3.forward * _moveInput.y), playerMoveDamp.Value);
            controller.Move(_move * (currentPlayerSpeed * Time.fixedDeltaTime));
        }



        private void RotatePlayerObj()
        {
            if(_playerLook.magnitude > 0.1f)
            {
                playerObjTransform.forward = new Vector3(_playerLook.x, 0, _playerLook.y);
            }
        }



        private void SetAnimatorMove()
        {  
            animator.SetBool("IsMoving", currentCanMove && isGrounded && _move.magnitude > 0.1f);
        }



        private void SetWalkingSound()
        {
            if(currentCanMove && isGrounded && _move.magnitude > 0.1f)
            {
                if(!walkEmitter.IsPlaying())
                {
                    walkEmitter.Play();
                }
            } else
            {
                if(walkEmitter.IsPlaying())
                {
                    walkEmitter.Stop();
                }
            }
        }
        
        

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if(currentCanMove)
            {
                _moveInput = ctx.ReadValue<Vector2>();
                if(_lookInput.magnitude < 0.1f)
                {
                    _playerLook = ctx.ReadValue<Vector2>();
                }
            } else
            {
                _moveInput = Vector2.zero;
            }
        }
        
        
        
        public void ToggleIsControllable(bool isControllable)
        {
            currentCanMove = isControllable && canMove.Value;

            if(!isControllable)
            {
                walkEmitter.Stop();
            }
        }



    }
}