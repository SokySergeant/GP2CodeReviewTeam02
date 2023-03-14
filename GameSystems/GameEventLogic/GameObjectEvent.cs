using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.GameEventLogic {
    [CreateAssetMenu(fileName = "New GameObjectEvent", 
        menuName = "Designer Tools/Events Logic/Game Object Event")] 
    public class GameObjectEvent : GameEvent
    {
        private readonly List<GameObjectEventListener> _eventListeners = 
            new List<GameObjectEventListener>();

        public void Raise(GameObject obj)
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(obj);
        }

        public void RegisterListener(GameObjectEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameObjectEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener); 
        }
    }
}

