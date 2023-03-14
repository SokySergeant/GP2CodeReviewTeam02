using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenCameraInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput p1Input;
    [SerializeField] private PlayerInput p2Input;
    [SerializeField] private CinemachineInputProvider p1Provider;
    [SerializeField] private CinemachineInputProvider p2Provider;
    
    
    void Start()
    {
        p1Provider.PlayerIndex = p1Input.playerIndex;
        p2Provider.PlayerIndex = p2Input.playerIndex;
    }
}
