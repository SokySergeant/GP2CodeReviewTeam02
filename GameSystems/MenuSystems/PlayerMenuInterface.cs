using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystems.MenuSystems {
    public class PlayerMenuInterface : MonoBehaviour {
        [SerializeField]private GameMenuReference _gameMenuReference;
        private PlayerInput _playerInput;
        private bool _menuOpen = false;
        void Start() {
            _playerInput = GetComponent<PlayerInput>();
        }
        public void OnToggleMenu(InputAction.CallbackContext ctx)
        {
            if(!ctx.started) return;
            ToggleMenu();
        }
        
        private void ToggleMenu() {
            _gameMenuReference.ToggleMenu();
        }
     
        
        
    }
}
