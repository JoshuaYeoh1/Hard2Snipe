using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibbedScript : MonoBehaviour
{
    public GameObject[] gibsPrefab;

    bool onlyOnce=true;

    public void gibbed()
    {
        if(onlyOnce)
        {
            onlyOnce=false;

            for(int i=0;i<gibsPrefab.Length;i++)
            {
                Instantiate(gibsPrefab[i],transform.position,transform.rotation);
            }
        }
        
    }
}
