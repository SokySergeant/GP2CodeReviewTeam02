using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    // [SerializeField] private CinemachineBrain _pCamBrain1;
    // [SerializeField] private CinemachineBrain _pCamBrain2;
     [SerializeField] private Camera _pCam1;
     [SerializeField] private CinemachineVirtualCamera topDownCam;

    void Start() {
        topDownCam.Priority = 11;
        StartCoroutine(ScreenTransition(0.5f, 0.5f, 1f));
    }
    
    private IEnumerator ScreenTransition(float screenChangeDuration, float startValue = 0f, float endValue = 1f) {
        float time = 0;
        var RectAsStruct = _pCam1.rect;
        float playerCamViewWidth = RectAsStruct.width;
        RectAsStruct.width = 0.2f;
        _pCam1.rect = RectAsStruct;
        while (time < screenChangeDuration)
        {
            RectAsStruct = _pCam1.rect;
            RectAsStruct.width  = (Mathf.Lerp(startValue, endValue, time / screenChangeDuration));
            time += Time.deltaTime;
            _pCam1.rect = RectAsStruct;
            yield return null;
        }
        RectAsStruct = _pCam1.rect;
        RectAsStruct.width  = endValue;
        _pCam1.rect = RectAsStruct;
        yield return null;
    }
    
    // private IEnumerator Lerp(FloatVariable valueToChange, float duration, float startValue = 0f, float endValue = 1f) {
    //     float time = 0;
    //     while (time < duration)
    //     {
    //         valueToChange.Value = (Mathf.Lerp(startValue, endValue, time / duration));
    //         time += Time.deltaTime;
    //         yield return null;
    //     }
    //     valueToChange.Value = endValue;
    //     yield return null;
    // }
}
