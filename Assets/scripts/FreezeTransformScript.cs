using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTransformScript : MonoBehaviour
{

    public bool freezeRotation=true;

    void LateUpdate()
    {
        if(freezeRotation)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
