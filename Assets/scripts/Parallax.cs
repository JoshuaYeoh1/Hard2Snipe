using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    float startPos, length;

    public float parallaxEffect;

    public Camera cam;

    void Start()
    {
        startPos = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        float temp = (cam.transform.position.x*(1-parallaxEffect));

        float distance = (cam.transform.position.x*parallaxEffect);

        transform.position = new Vector2(startPos+distance,transform.position.y);

        if(temp>startPos+length)
        {
            startPos+=length;
        }
        else if(temp<startPos-length)
        {
            startPos-=length;
        }
    }

}
