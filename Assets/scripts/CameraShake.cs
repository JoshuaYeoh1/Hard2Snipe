using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public GameObject camholder;

    [SerializeField]
    private float frequency, magnitude, duration;
    
    bool shaking;

    float seed;

    void Awake()
    {
        seed = Random.value;
    }

    void LateUpdate()
    {
        if(shaking)
        {
            transform.localPosition = new Vector2(Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1, Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1) * magnitude;
        }
    }

    public void shake()
    {
        StartCoroutine(shaker());
    }

    IEnumerator shaker()
    {
        transform.position = camholder.transform.position;

        shaking=true;

        yield return new WaitForSeconds(duration);

        shaking=false;

        transform.position = camholder.transform.position;
    }
}
