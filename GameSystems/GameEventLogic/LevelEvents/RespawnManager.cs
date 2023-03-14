using System;
using GameSystems.AgentLogic;
using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.GameEventLogic.LevelEvents {
    [CreateAssetMenu(menuName = "Programmer Tools/Respawn Manager",fileName = "New Respawn Manager")]
    public class RespawnManager : ScriptableObject {
        [SerializeField] private RespawnPoint _currentPlayer1RespawnPoint;
        [SerializeField] private RespawnPoint _currentPlayer2RespawnPoint;
        [SerializeField] private FloatVariable _respawnTimer;
        private int _playerID = 1;
        
        public void HandleRespawnPlayer(PlayerManager player) {
            var currentRespawnPoint = GetRespawnPoint(player.PlayerId);
            var newForward = currentRespawnPoint.transform.forward;
            var resTransform = currentRespawnPoint.RespawnPosition.position;
            player.RespawnPlayer(_respawnTimer.Value,resTransform,newForward);
        }
        
        public void HandleRespawnPlayer(PlayerManager player, float duration) {
            var currentRespawnPoint = GetRespawnPoint(player.PlayerId);
            var newForward = currentRespawnPoint.transform.forward;
            var resTransform = currentRespawnPoint.RespawnPosition.position;
            player.RespawnPlayer(duration,resTransform,newForward);
        }

        
        private RespawnPoint GetRespawnPoint(int id) {
            if (id == 1) {
                return _currentPlayer1RespawnPoint;
            }
            else {
                return _currentPlayer2RespawnPoint;

            }
        }
        
        public void RegisterNewResPoint(int playerID, RespawnPoint newRespawnPoint) {
            if (playerID == 1) {
                _currentPlayer1RespawnPoint = newRespawnPoint;
            }
            else if (playerID == 2){
                _currentPlayer2RespawnPoint = newRespawnPoint;
            }
        }

        private void OnDisable() {
            _playerID = 1;
        }
        private void ResetID() {
            _playerID = 1;
        }

        public int GetPlayerId() {
            return _playerID++;
        }
        
        
        
    }
}
