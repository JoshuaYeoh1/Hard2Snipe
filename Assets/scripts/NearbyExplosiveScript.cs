using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyExplosiveScript : MonoBehaviour
{   
    GameObject explosive;

    public ScopeBehaviour scopeBehaviour;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "explosive")
        {
            explosive = other.gameObject;

            scopeBehaviour.aimExplosive(explosive);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "explosive")
        {
            explosive = null;

            scopeBehaviour.aimExplosive(explosive);
        }
    }
}
