using GameSystems.AgentLogic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.GameEventLogic {
    public class GamePlayerEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GamePlayerEvent Event;
    
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<PlayerManager> Response;
    
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }
    
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    
        public void OnEventRaised(PlayerManager obj)
        {
            Response.Invoke(obj);
        }
    }
}