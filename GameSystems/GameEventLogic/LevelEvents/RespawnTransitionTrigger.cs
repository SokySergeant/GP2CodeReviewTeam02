using System;
using GameSystems.AgentLogic;
using UnityEditor;
using UnityEngine;

namespace GameSystems.GameEventLogic.LevelEvents {
    public class RespawnTransitionTrigger : MonoBehaviour {
        [SerializeField] private RespawnPoint _newRespawnPointForPlayer1;
        [SerializeField] private RespawnPoint _newRespawnPointForPlayer2;
        [SerializeField]private RespawnManager _respawnManager;
        
        // Start is called before the first frame update
        void Awake()
        {
            // _respawnManager =
            //     AssetDatabase.LoadAssetAtPath<RespawnManager>(
            //         "Assets/GameSystems/GameEventLogic/LevelEvents/RespawnManager.asset");
        }
        

        public void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                int playerID = other.GetComponent<PlayerManager>().PlayerId;
                print("Updating respawn point for player " + playerID);
                var respawnPoint = GetRespawnPoint(playerID);
                _respawnManager.RegisterNewResPoint(playerID,respawnPoint);
            }
        }
        
        private RespawnPoint GetRespawnPoint(int id) {
            if (id == 1) {
                return _newRespawnPointForPlayer1;
            }
            else {
                return _newRespawnPointForPlayer2;

            }
        }
    }
}
