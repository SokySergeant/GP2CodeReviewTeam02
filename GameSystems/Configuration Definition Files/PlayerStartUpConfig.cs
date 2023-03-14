using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStartUpConfig : MonoBehaviour {
     [SerializeField] private string _playerTag;
    private void OnEnable() {
        gameObject.tag = _playerTag;
    }
}
