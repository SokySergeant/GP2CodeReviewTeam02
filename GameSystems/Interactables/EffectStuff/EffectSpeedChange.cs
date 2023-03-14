using System.Collections;
using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.Interactables.EffectStuff {
    public class EffectSpeedChange : EffectAbstract
    {
        [Tooltip("Multiplier to the player's speed")] [SerializeField] [Range(0, 2)] private float  playerSpeedChangePercentage;
        [SerializeField] private float secondsToBeOnFor;
        [SerializeField] private float secondsToBeOffFor;
        [SerializeField] private FloatVariable playerSpeed;

        private bool _isOn = true;
    


        private void OnEnable()
        {
            StartCoroutine(CycleIsOn());
        }

    
    
        private IEnumerator CycleIsOn()
        {
            while(true)
            {
                yield return new WaitForSeconds(secondsToBeOnFor);
            
                _isOn = false;
                for(int i = 0; i < _players.Count; i++)
                {
                    _players[i].GetComponent<PlayerMovement.PlayerMovement>().currentPlayerSpeed = playerSpeed.Value;
                }

                yield return new WaitForSeconds(secondsToBeOffFor);
            
                _isOn = true;
                for(int i = 0; i < _players.Count; i++)
                {
                    _players[i].GetComponent<PlayerMovement.PlayerMovement>().currentPlayerSpeed *= playerSpeedChangePercentage;
                }
            }
        }
    


        protected override void OnPlayerEnter(GameObject player)
        {
            if(!_isOn) return;
        
            PlayerMovement.PlayerMovement playerMovement = player.GetComponent<PlayerMovement.PlayerMovement>();
            playerMovement.currentPlayerSpeed *= playerSpeedChangePercentage;
        }

        protected override void OnPlayerExit(GameObject player)
        {
            PlayerMovement.PlayerMovement playerMovement = player.GetComponent<PlayerMovement.PlayerMovement>();
            playerMovement.currentPlayerSpeed = playerSpeed.Value;
        }
    }
}
