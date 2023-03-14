using GameSystems.RuntimeSets;
using UnityEngine;

namespace GameSystems.UpdateLoopsUtilities {
    public abstract class MonoSlowable : MonoBehaviour {
        [SerializeField] private BaseRuntimeSet<MonoSlowable> _runtimeSet = null;

        public abstract void GameUpdate(float deltaTime, float slowDownFactor);
        void Update()
        {
        
        }
    }
}
