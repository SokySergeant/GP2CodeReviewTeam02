using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.GameEventLogic {
    [CreateAssetMenu(fileName = "New GameObjectEvent", 
        menuName = "Designer Tools/Events Logic/Game Object Event")] 
    public class GameVector3Event : GameEvent
    {
        private readonly List<GameVector3EventListener> _eventListeners = 
            new List<GameVector3EventListener>();

        public void Raise(Vector3 vec)
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(vec);
        }

        public void RegisterListener(GameVector3EventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameVector3EventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener); 
        }
    }
}