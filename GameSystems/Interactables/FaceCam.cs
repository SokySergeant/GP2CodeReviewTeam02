using UnityEngine;

public class FaceCam : MonoBehaviour
{
    [SerializeField] private Vector3 eulerOffset;
    private Camera _mainCam;

    

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        Vector3 newRotation = _mainCam.transform.eulerAngles;
        newRotation.y = 0;
        newRotation.z = 0;
        transform.eulerAngles = newRotation + eulerOffset;
    }
}
