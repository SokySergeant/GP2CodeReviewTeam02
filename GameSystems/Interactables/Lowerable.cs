using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lowerable : MonoBehaviour {
    [SerializeField] private Transform _blockingObject;
    
   
    public void Lower(float sinkAmount) {
        StartCoroutine(LowerAsCoroutine(sinkAmount));
    }
    private IEnumerator LowerAsCoroutine(float sinkAmount) {
        float CurrentYValue = 0f;
        float targetYValue = 0f;
        CurrentYValue = _blockingObject.position.y;
        targetYValue = CurrentYValue - sinkAmount;
        while (_blockingObject.position.y > targetYValue) {
            _blockingObject.Translate(Vector3.down * Time.deltaTime);
            yield return null;
        }
    }
}
