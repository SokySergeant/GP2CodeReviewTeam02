using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.up,Time.deltaTime *10f);
    }
}
