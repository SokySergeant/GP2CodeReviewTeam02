using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.MenuSystems {
    public class GameMenuController : MonoBehaviour {

        [SerializeField] private GameMenuReference _menuReference;
        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _inGameButtonPanel;
        [SerializeField] private GameObject _inGameSettingsPanel;
        // Start is called before the first frame update
        private void Awake() {
            _menuReference.Configure(this);
        }

        public void OpenIngameMenu() {
            _inGamePanel.SetActive(true);
            _inGameButtonPanel.SetActive(true);
            _inGameButtonPanel.GetComponentInChildren<Button>().Select();

        }
        
        public void ToggleSettings(bool newSettingsState) {
            _inGameButtonPanel.SetActive(!newSettingsState);
            _inGameSettingsPanel.SetActive(newSettingsState);
            if (newSettingsState) {
                _inGameSettingsPanel.GetComponentInChildren<Button>().Select();
            }
            else {
                _inGameButtonPanel.GetComponentInChildren<Button>().Select();

            }
        }

        public void CloseIngameMenu() {
            _inGamePanel.SetActive(false);
            _inGameButtonPanel.SetActive(false);
            _inGameSettingsPanel.SetActive(false);
        }
    }
}
