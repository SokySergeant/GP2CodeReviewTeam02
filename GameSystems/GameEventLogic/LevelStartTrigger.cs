using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.GameEventLogic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelStartTrigger : MonoBehaviour {
    [SerializeField] private GameEvent _onPlayerEnteringForFirstTime;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            _onPlayerEnteringForFirstTime.Raise();
        }
    }
}
