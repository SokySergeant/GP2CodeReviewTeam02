using System;
using GameSystems.GameEventLogic;
using UnityEngine;

namespace GameSystems.SceneLoading.SOImplementations {
    public class SceneLoadMono : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader = null;
        [SerializeField] private GameEvent _onStartUp = null;

        public void Awake() {
            _onStartUp.Raise();
        }
    }
}
