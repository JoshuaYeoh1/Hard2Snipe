using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Camera cam;

    public GameObject player;

    Vector2 mousePos;

    public int divide;

    float distX,distY;

    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        distX = mousePos.x-player.transform.position.x;
        distY = mousePos.y-player.transform.position.y;

        transform.position = new Vector2(player.transform.position.x+(distX/divide),player.transform.position.y+(distY/divide));

        if(transform.position.x<=minX)
        {
            transform.position = new Vector2(minX,transform.position.y);
        }
        else if(transform.position.x>=maxX)
        {
            transform.position = new Vector2(maxX,transform.position.y);
        }

        if(transform.position.y<=minY)
        {
            transform.position = new Vector2(transform.position.x,minY);
        }
        else if(transform.position.y>=maxY)
        {
            transform.position = new Vector2(transform.position.x,maxY);
        }
    }
}
