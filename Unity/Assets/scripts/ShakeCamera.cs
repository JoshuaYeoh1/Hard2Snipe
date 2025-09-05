using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    CameraShake cameraShake;

    void Awake()
    {
        cameraShake = GameObject.FindGameObjectWithTag("camshaker").GetComponent<CameraShake>();
        cameraShake.shake();
    }
}
