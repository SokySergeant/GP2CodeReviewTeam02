using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.GameEventLogic {
    [CreateAssetMenu(menuName = "Designer Tools/Events Logic/Game Float Event", fileName = "New Float Event")]

    public class GameFloatEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameFloatEventListener> _eventListeners = 
            new List<GameFloatEventListener>();

        public void Raise(float passedObj)
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(passedObj);
        }

        public void RegisterListener(GameFloatEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameFloatEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener); 
        }
    }
}