using System.Collections;
using GameSystems.Functions;
using UnityEngine;

namespace GameSystems.Interactables {
    public class WaterController : Interactable
    {
        [Header("Water Controller Variables")]
        [SerializeField] private float _riseRate = 0f;
        [SerializeField] private float _sinkAmount = 0f;
        [SerializeField] private Transform _waterPlane;
        private float _targetHeight = 0f;
        [SerializeField] private float _minHeight = -5f;
        [SerializeField] private float _maxHeight = 5f;
        [SerializeField] private bool _isRising = false;
        [SerializeField] private bool _hasWaterPlane = false;
        [SerializeField] private LevelIndicator _levelIndicator;
        private float _minClampValue;
        private float _maxClampValue;
        
        [Header("Water Gushing VFX Variables")]
        [SerializeField] private ParticleSystem waterGushingVfx;
        [SerializeField] private float waterGushingStopTime = 1;
        private float _currentWaterGushingTime;

        private Vector3 _changeAmount;
        private float defaultYValue;
        private float CurrentYValue;
        private float targetYValue;
        private float _timeElapsed;
        
        
        
        protected override void  OnEnableForInheriting() {
            if (_waterPlane == null) {
                _hasWaterPlane = false;
                return;
            } else
            {
                _hasWaterPlane = true;
            }

            defaultYValue = _waterPlane.localPosition.y;
            CurrentYValue = _waterPlane.localPosition.y;
            targetYValue = CurrentYValue;
            _minClampValue = CurrentYValue + _minHeight;
            _maxClampValue = CurrentYValue + _maxHeight;
            _changeAmount = new Vector3(0, 1, 0);
        }


        
        void Update() {
            if (_isRising && _hasWaterPlane) {
                _timeElapsed = Time.deltaTime;
                RaiseWaterPLane(_timeElapsed);
                float waterLevel = GenericFunctions.Remap(_minClampValue, _maxClampValue, 0f, 10f, CurrentYValue);
                float waterGradient = GenericFunctions.Remap(_minClampValue, _maxClampValue, -0.5f, 0.5f, CurrentYValue);
            
         
                if (_levelIndicator != null) {
                    _levelIndicator.UpdateLevel(waterLevel,waterGradient);
                }
            }
        }

        private void RaiseWaterPLane(float timeElapsed) {
            
            targetYValue += (_riseRate * timeElapsed);
            targetYValue = Mathf.Clamp(targetYValue, _minClampValue, _maxClampValue);
            CurrentYValue = _waterPlane.localPosition.y;
            
            if (CurrentYValue < targetYValue)
            {
                _waterPlane.position += _changeAmount * (_riseRate * timeElapsed);

            } 
            else if(CurrentYValue > targetYValue)
            {
                _waterPlane.position -= _changeAmount * (_riseRate * timeElapsed);

            }
        }
        
        
        
        protected override void TurnOnForInheriting()
        {
            SinkWaterPlane();
        }

        

        protected override void ResetSelfForInheriting() {
            if (_waterPlane == null) return;
            
            _waterPlane.localPosition = new Vector3(_waterPlane.localPosition.x, defaultYValue, _waterPlane.localPosition.z);

            _isRising = false;
            
            CurrentYValue = defaultYValue;
            targetYValue = CurrentYValue;
            _minClampValue = CurrentYValue + _minHeight;
            _maxClampValue = CurrentYValue + _maxHeight;
            _changeAmount = new Vector3(0, 1, 0);
        }

        

        public void ToggleIsRising(bool isRising) {
            _isRising = isRising;
        }

        
        
        [ContextMenu("Sink Plane")]
        public void SinkWaterPlane() {
            targetYValue -= _sinkAmount;

            if(waterGushingVfx == null) return;
            _currentWaterGushingTime = waterGushingStopTime;
            if(!_waterGushingIsBusy)
            {
                StartCoroutine(TemporarilyTurnOffWaterVfx());
            }
        }

        private bool _waterGushingIsBusy = false;
        private IEnumerator TemporarilyTurnOffWaterVfx()
        {
            _waterGushingIsBusy = true;
            
            waterGushingVfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            while(_currentWaterGushingTime > 0)
            {
                _currentWaterGushingTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            waterGushingVfx.Play();

            _waterGushingIsBusy = false;
        }
        
        
        public void SinkWaterPlanePermanently(float sinkAmount) {
            StartCoroutine(SinkWaterPlaneAsCoroutine(sinkAmount));
        }
        private IEnumerator SinkWaterPlaneAsCoroutine(float sinkAmount) {
            
            CurrentYValue = _waterPlane.position.y;
            targetYValue = CurrentYValue - sinkAmount;
            while (_waterPlane.position.y > targetYValue) {
                _waterPlane.Translate(Vector3.down * Time.deltaTime);
                yield return null;
            }
        }
    }
}
