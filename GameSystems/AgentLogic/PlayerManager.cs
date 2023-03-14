using System.Collections;
using FMODUnity;
using GameSystems.GameEventLogic;
using GameSystems.GameEventLogic.LevelEvents;
using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.AgentLogic {
    public class PlayerManager : MonoBehaviour {
        
        [Header("Serialized References")]
        [SerializeField] private Transform groundCheck;
        
        [Header("References")]
        [SerializeField] private RespawnManager _respawnManagerReference;
        private CharacterController _characterController = null;
        private PlayerMovement.PlayerMovement _playerMovement = null;
        
        [Header("Events")] 
        [SerializeField] private GamePlayerEvent _onKilled;

        [Header("Masks")]
        [SerializeField] private LayerMask whatIsResPlatform;
        [SerializeField] private LayerMask whatIsGround;

        [Header("State")]
        public int PlayerId = 0;
        private float _revivalTimer = 0f;
        public bool _isOnNormalGround = false;
        private float _groundRadius = 0.5f;
        public bool IsResurrecting { get; set; } = false;
        public bool IsActive = true;

        [Header("Studio Event Emitter References")] 
        [SerializeField] private StudioEventEmitter respawnEmitter;
        
        public delegate void OnDeath();
        public OnDeath onDeath;
        public delegate void OnRevive();
        public OnRevive onRevive;

        public Coroutine respawnRoutine;
        public bool breakOutOfRespawnCoroutine = false;

        public InteractablePipe pipe;


        
        void Start() {
            Initialize();
        }

        void FixedUpdate() {
            SetIsOnNormalGround();
            SetIsResurrecting();
        }

        private void SetIsResurrecting() {
            if (_isOnNormalGround) {
                IsResurrecting = false;
            }
        }

        private void SetIsOnNormalGround() {
            _isOnNormalGround = Physics.CheckSphere(groundCheck.position, _groundRadius, whatIsGround);
        }

        public void KillPlayer() {
            if(pipe != null)
            {
                pipe.ReleaseCrawler();
            }

            _respawnManagerReference.HandleRespawnPlayer(this);
            
            onDeath?.Invoke();
        }
        
        public void OnGameOverRespawn(float duration) {
            _respawnManagerReference.HandleRespawnPlayer(this, duration);
        }

        public void RespawnPlayer(float respawnDuration, Vector3 resPosition, Vector3 newForward) {
            
            Vector3 startPosition = resPosition + new Vector3(0, 1.5f, 0);
            Vector3 endPosition = resPosition;
            respawnRoutine = StartCoroutine(RespawnCoroutine(respawnDuration, startPosition,endPosition));
        }

        public IEnumerator RespawnCoroutine(float respawnDuration, Vector3 resStartPosition, Vector3 resEndPosition) {
            _characterController.enabled = false;
            _playerMovement.ToggleIsControllable(false);
            IsResurrecting = true;
            _revivalTimer = 0f;
            float t;
         
            _characterController.transform.position = resStartPosition;
            while (!breakOutOfRespawnCoroutine && _revivalTimer < respawnDuration) {
                _revivalTimer += Time.deltaTime;
                t = _revivalTimer / respawnDuration;
                _characterController.transform.position = Vector3.Lerp(resStartPosition, resEndPosition, t);
                yield return null;
            }

            if(breakOutOfRespawnCoroutine)
            {
                yield return 0;
            }
            
            _characterController.transform.position = resEndPosition;
            
            _playerMovement.ToggleIsControllable(true);
            _characterController.enabled = true;

            onRevive?.Invoke();
            respawnEmitter.Play();
            
            yield return null;
        }

        private void Initialize() {
            _revivalTimer = 0f;
            IsActive = true;
            PlayerId = _respawnManagerReference.GetPlayerId();
            _characterController = GetComponent<CharacterController>();
            _playerMovement = GetComponent<PlayerMovement.PlayerMovement>();

        }
    }
}