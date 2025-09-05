using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnerScript : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] coverProps, explosiveProps, barricadeProps, helpfulProps;

    public int rarity;

    void Awake()
    {
        for(int i=0;i<spawnPoints.Length;i++)
        {
            if(Random.Range(1,rarity+1) == 1)
                spawn();
        }
    }

    void spawn()
    {
        int iCover = Random.Range(0,coverProps.Length);
        int iExplosive = Random.Range(0,explosiveProps.Length);
        int iBarricade = Random.Range(0,barricadeProps.Length);
        int iHelpful = Random.Range(0,helpfulProps.Length);
        int iSpawnpoint = Random.Range(0,spawnPoints.Length);

        if(Random.Range(1,7)==1)
        { 
            if(Random.Range(1,3)==1)
            { 
                Instantiate(explosiveProps[iExplosive], spawnPoints[iSpawnpoint].position, transform.rotation);
            }
            else
            {
                Instantiate(barricadeProps[iBarricade], spawnPoints[iSpawnpoint].position, transform.rotation);
            }
        }
        else if(Random.Range(1,5)==1)
        {
            Instantiate(helpfulProps[iHelpful], spawnPoints[iSpawnpoint].position, transform.rotation);
        }
        else
        {
            Instantiate(coverProps[iCover], spawnPoints[iSpawnpoint].position, transform.rotation);
        }
    }
}
