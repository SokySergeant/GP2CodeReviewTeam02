using System.Collections.Generic;
using GameSystems.AgentLogic;
using UnityEngine;

namespace GameSystems.GameEventLogic {
    [CreateAssetMenu(fileName = "New GamePlayerEvent", 
        menuName = "Designer Tools/Events Logic/Game Player Event")] 
    public class GamePlayerEvent : GameEvent
    {
        private readonly List<GamePlayerEventListener> _eventListeners = 
            new List<GamePlayerEventListener>();
        [ContextMenu("Raise")]
        public void Raise(PlayerManager obj)
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(obj);
        }

        public void RegisterListener(GamePlayerEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GamePlayerEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener); 
        }
    }
}