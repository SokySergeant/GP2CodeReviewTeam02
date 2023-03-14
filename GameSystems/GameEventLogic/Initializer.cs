using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.GameEventLogic;
using UnityEngine;

public class Initializer : MonoBehaviour {

    [SerializeField] private GameEvent[] _onStartEvents;
    public void Awake() {
        foreach (var onStartEvent in _onStartEvents) {
            onStartEvent.Raise();
        }
    }
    
    
}
