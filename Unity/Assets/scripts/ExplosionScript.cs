using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    AudioManager audioManager;

    public AudioClip[] sndExplosion;

    void Awake()
    {   
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        audioManager.playSFX(sndExplosion, transform, 1f);
    }
}
