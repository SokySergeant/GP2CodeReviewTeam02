using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.GameEventLogic {
    public class GameFloatEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameFloatEvent Event;
    
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<float> Response;
    
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }
    
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    
        public void OnEventRaised(float value)
        {
            Response.Invoke(value);
        }
    }
}