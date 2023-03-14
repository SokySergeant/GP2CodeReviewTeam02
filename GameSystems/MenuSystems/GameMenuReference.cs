using GameSystems.GameEventLogic;
using GameSystems.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace GameSystems.MenuSystems {
    [CreateAssetMenu(menuName = "Programmer Tools/Game Menus/Game Menu Reference",fileName = "New Game Menu Reference")]
    public class GameMenuReference : ScriptableObject {
        private GameMenuController _gameMenuController;
        [SerializeField]private GameEvent _OnGameMenuOpened;
        [SerializeField]private GameEvent _OnGameMenuClosed;
        private bool _menuOpen = false;

        public void OpenIngameMenu() {
            _OnGameMenuOpened.Raise();
            _gameMenuController.OpenIngameMenu();
            _menuOpen = true;
            Time.timeScale = 0f;
        }
        
        public void CloseGameMenu() {
            _OnGameMenuClosed.Raise();
            _menuOpen = false;
            Time.timeScale = 1f;
            _gameMenuController.CloseIngameMenu();


        }

        public void RestoreSettingsOnExit() {
            Time.timeScale = 1f;
        }

        public void Configure(GameMenuController controller) {
            _gameMenuController = controller;
        }

        public void ToggleMenu() {
            if (_menuOpen) {
                CloseGameMenu();
            }
            else {
                OpenIngameMenu();
            }
        }
    }
}
