using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.GameEventLogic {
    public class GameVector3EventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameVector3Event Event;
    
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<Vector3> Response;
    
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }
    
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    
        public void OnEventRaised(Vector3 vec)
        {
            Response.Invoke(vec);
        }
    }
}