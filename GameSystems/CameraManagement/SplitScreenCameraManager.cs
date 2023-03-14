using System.Collections;
using Cinemachine;
using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.CameraManagement {
    public class SplitScreenCameraManager : MonoBehaviour {
        [SerializeField] private CinemachineBrain _pCamBrain1;
        [SerializeField] private CinemachineBrain _pCamBrain2;
        [SerializeField] private Camera _pCam1;
        [SerializeField] private FloatVariable _cameraModeTransitionTime;
        [SerializeField] private Transform _cameraRotationAnchor;
    
        private CameraMode _currentCameraMode = CameraMode.TopDown;
        void Start() {
            // Room1Cam.Priority = 11;
            // StartCoroutine(ScreenTransition(0.5f, 0.5f, 1f));
        }

        public void LateUpdate() {
            _cameraRotationAnchor.Rotate(_cameraRotationAnchor.up,Time.deltaTime *10f);
        }

        public void ChangeCameraMode(CameraMode newCameraMode) {
            if (_currentCameraMode == newCameraMode) {
                return;
            }
        
            _currentCameraMode = newCameraMode;
            SetCameraPriority(newCameraMode);
            TriggerScreenTransition(newCameraMode);
        
        }
        private void SetCameraPriority(CameraMode newCameraMode) {
            switch (newCameraMode) {
            
                case CameraMode.TopDown:
                    //_room1Cam.Priority = 11;
                    break;
                case CameraMode.SplitScreenEven:
                    //_room1Cam.Priority = 1;
                    break;
            
                default: return;
            }
        }
        
        private void TriggerScreenTransition(CameraMode newCameraMode) {
            switch (newCameraMode) {
            
                case CameraMode.TopDown:
                     StartCoroutine(ScreenTransition(0.5f, 1f));
                     break;
                case CameraMode.SplitScreenEven:
                     StartCoroutine(ScreenTransition(1.0f, 0.5f));
                     break;
            
                default: return;
            }
        }
        
        
        
        private IEnumerator ScreenTransition(float startValue = 0f, float endValue = 1f) {
            float time = 0;
            float screenChangeDuration = _cameraModeTransitionTime.Value;
            var RectAsStruct = _pCam1.rect;
            float playerCamViewWidth = RectAsStruct.width;
            RectAsStruct.width = 0.2f;
            _pCam1.rect = RectAsStruct;
            while (time < screenChangeDuration)
            {
                RectAsStruct = _pCam1.rect;
                RectAsStruct.width  = (Mathf.SmoothStep(startValue, endValue, time / screenChangeDuration));
                time += Time.deltaTime;
                _pCam1.rect = RectAsStruct;
                yield return null;
            }
            RectAsStruct = _pCam1.rect;
            RectAsStruct.width  = endValue;
            _pCam1.rect = RectAsStruct;
            yield return null;
        }
    
        private IEnumerator Lerp(FloatVariable valueToChange, float duration, float startValue = 0f, float endValue = 1f) {
            float time = 0;
            while (time < duration)
            {
                valueToChange.Value = (Mathf.Lerp(startValue, endValue, time / duration));
                time += Time.deltaTime;
                yield return null;
            }
            valueToChange.Value = endValue;
            yield return null;
        }
    }
}
