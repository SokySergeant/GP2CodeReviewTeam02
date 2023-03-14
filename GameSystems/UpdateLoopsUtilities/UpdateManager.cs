using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.UpdateLoopsUtilities {
    [CreateAssetMenu(fileName = "Update Manager", 
        menuName = "Programmer Tools/UpdateManager", order = 0)]
    public class UpdateManager : ScriptableObject
    {
        
        private bool _paused = false;

        public bool Paused => _paused;

        private UpdateRelay _relay = null;
        
        public void Initialise(UpdateRelay relay){
            //_input.AddOnMenuToggleInput(OnMenuToggleEvent);
            _relay = relay;
        }
        
        public void StopCoroutine(Coroutine routine){
            if (routine == null) return;
            _relay.StopCoroutine(routine);
        }
        public Coroutine StartCoroutine(IEnumerator routine){
            return _relay.StartCoroutine(routine);
        }
        public void OnDestroy() {
            
        }
    }
}
