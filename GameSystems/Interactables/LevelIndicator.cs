using System;
using System.Collections;
using GameSystems.Functions;
using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.Interactables {
    public class LevelIndicator : MonoBehaviour {
        [SerializeField] private Transform _indicator;
        [SerializeField] private Transform _sliderBody;
        [SerializeField] private Renderer _sliderBodyMesh;
        
       private MaterialPropertyBlock _sliderColor;
        private Vector3 _position;
        

        private void Awake() {
            _sliderColor = new MaterialPropertyBlock();
            _sliderBodyMesh.SetPropertyBlock(_sliderColor);
        }

        public void UpdateLevel(float indicatorValue,float waterGradient) {
            var localPosition = _indicator.localPosition;
            _position =  new Vector3(localPosition.x,indicatorValue,localPosition.z);
            localPosition = _position;
            _indicator.localPosition = localPosition;
            ConfigureColor(waterGradient);

        }

        public void ConfigureColor(float currentValue) {
            _sliderColor.SetFloat("_WaterHeight",currentValue);
            _sliderBodyMesh.SetPropertyBlock(_sliderColor);

        }
    }
}