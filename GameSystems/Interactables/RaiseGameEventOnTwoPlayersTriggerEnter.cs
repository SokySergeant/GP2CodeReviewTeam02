using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.AgentLogic;
using GameSystems.GameEventLogic;
using UnityEngine;

namespace GameSystems.Interactables {
    public class RaiseGameEventOnTwoPlayersTriggerEnter : MonoBehaviour
    {
        [SerializeField] private LayerMask whatCanTrigger;
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private bool repeatable = false;
        [SerializeField] private float secondsToWaitOnStartup = 0;
        private HashSet<GameObject> players = new HashSet<GameObject>();
        private bool _canTrigger = true;
        private bool _isBusy = false;
        


        private void OnTriggerEnter(Collider other)
        {
            if(!_canTrigger || _isBusy) return;

            if((whatCanTrigger & (1 << other.gameObject.layer)) != 0) {
                if(!players.Contains(other.gameObject))
                {
                    players.Add(other.gameObject);
                }
            }
            if (players.Count == 2) {
                StartCoroutine(DoTrigger());
            }
            
        }  
        private void OnTriggerExit(Collider other)
        {
            if(!_canTrigger || _isBusy) return;

            if((whatCanTrigger & (1 << other.gameObject.layer)) != 0) {
                if(players.Contains(other.gameObject))
                {
                    players.Remove(other.gameObject);
                }
            }
            
        }
        private IEnumerator DoTrigger()
        {
            _isBusy = true;
        
            yield return new WaitForSeconds(secondsToWaitOnStartup);

            gameEvent.Raise();

            if(!repeatable)
            {
                _canTrigger = false;
            }

            _isBusy = false;
        }
    }
}