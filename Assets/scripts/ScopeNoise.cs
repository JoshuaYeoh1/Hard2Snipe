using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeNoise : MonoBehaviour
{

    public float randomOffset, frequency;

    float offsetNoiseX=0, offsetNoiseY=0, offsetNoiseDeg=0;

    void Awake()
    {
        StartCoroutine(aimingNoise());
    }

    IEnumerator aimingNoise()
    {
        while(true)
        {
            yield return new WaitForSeconds(frequency);

            offsetNoiseX = Random.Range(-randomOffset,randomOffset);
            offsetNoiseY = Random.Range(-randomOffset,randomOffset);
            offsetNoiseDeg = Random.Range(-randomOffset,randomOffset);

            transform.LeanMoveLocal(new Vector2(offsetNoiseX,offsetNoiseY),frequency).setEaseInOutSine();
            transform.LeanRotate(new Vector3(0,0,offsetNoiseDeg*5),frequency).setEaseInOutSine();
        } 
    }
}
