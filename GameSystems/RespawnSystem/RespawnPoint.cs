using System.Collections;
using System.Collections.Generic;
using GameSystems.AgentLogic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {
    [SerializeField] public Transform RespawnPosition = null;
    
    void Start()
    {
        if (RespawnPosition == null) {
            Debug.LogError(gameObject.name + " does not have a respawn position in its inspector");
        }
    }

}
