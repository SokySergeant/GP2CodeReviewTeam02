using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.GameEventLogic;
using UnityEngine;

public class CameraAngleTransitionTrigger : MonoBehaviour {
    public float TargetCameraAngle = 0f;
    [SerializeField] private GameFloatEvent _changeCameraOnEnter = null;
    private bool _hasActivated = false;
    private int _numEntered = 0;

    private void Awake() {
        _hasActivated = false;
        _numEntered = 0;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _numEntered++;
        }

        if (_numEntered > 2 && !_hasActivated) {
            _hasActivated = true;
            _changeCameraOnEnter.Raise(TargetCameraAngle);
        }
    }
}
