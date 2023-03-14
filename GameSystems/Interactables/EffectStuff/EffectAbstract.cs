using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Interactables.EffectStuff {
    public class EffectAbstract : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private LayerMask whatIsItem;

        protected List<GameObject> _players = new List<GameObject>();
        protected List<GameObject> _items = new List<GameObject>();
    
    
    

        private void OnTriggerEnter(Collider other)
        {
            if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
            {
                _players.Add(other.gameObject);
                OnPlayerEnter(other.gameObject);
            }

            if((whatIsItem & (1 << other.gameObject.layer)) != 0)
            {
                _items.Add(other.gameObject);
                OnItemEnter(other.gameObject);
            }
        }

        protected virtual void OnPlayerEnter(GameObject player) {}
        protected virtual void OnItemEnter(GameObject item) {}

    

        private void OnTriggerStay(Collider other)
        {
            if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
            {
                OnPlayerStay(other.gameObject);
            }
        
            if((whatIsItem & (1 << other.gameObject.layer)) != 0)
            {
                OnItemStay(other.gameObject);
            }
        }
    
        protected virtual void OnPlayerStay(GameObject player) {}
        protected virtual void OnItemStay(GameObject item) {}


    
        private void OnTriggerExit(Collider other)
        {
            if((whatIsPlayer & (1 << other.gameObject.layer)) != 0)
            {
                _players.Remove(other.gameObject);
                OnPlayerExit(other.gameObject);
            }
        
            if((whatIsItem & (1 << other.gameObject.layer)) != 0)
            {
                _items.Remove(other.gameObject);
                OnItemExit(other.gameObject);
            }
        }
    
        protected virtual void OnPlayerExit(GameObject player) {}
        protected virtual void OnItemExit(GameObject enemy) {}
    }
}
