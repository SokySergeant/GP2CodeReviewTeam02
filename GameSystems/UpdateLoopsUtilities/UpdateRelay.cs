using UnityEngine;

namespace GameSystems.UpdateLoopsUtilities 
{
    [AddComponentMenu("Managers/Update Relay", 0), DefaultExecutionOrder(-1)]
    public sealed class UpdateRelay : MonoBehaviour
    {
        [SerializeField] private UpdateManager _manager = null;
        private static UpdateRelay s_active = null;

        private void Awake(){
            if (s_active == null){
                _manager.Initialise(this);
                s_active = this;

            } else Destroy(this);
        }

        private void OnDestroy(){
            if (s_active != this) return;
            _manager.OnDestroy();
        }
    }
}