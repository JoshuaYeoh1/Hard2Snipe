using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{
    GibbedScript gibbedScript;

    AudioManager audioManager;

    public AudioClip[] sndAxeHit;
    public AudioClip[] sndBulletRic;
    public AudioClip[] sndBreak;
    public AudioClip[] sndBreak2;

    void Awake()
    {
        gibbedScript = GetComponent<GibbedScript>();

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "bullet")
        {
            StartCoroutine(delayBreak());

            audioManager.playSFX(sndBulletRic, transform, 1f);
        }
        else if(other.tag == "explosion")
        {
            StartCoroutine(delayBreak());
        }
        else if(other.tag == "axehitbox")
        {
            instaBreak();

            audioManager.playSFX(sndAxeHit, transform, 1f);
        }
    }

    IEnumerator delayBreak()
    {
        yield return new WaitForSeconds(.15f);

        instaBreak();
    }

    void instaBreak()
    {
        gibbedScript.gibbed();

        audioManager.playSFX(sndBreak, transform, 1f);
        audioManager.playSFX(sndBreak2, transform, 1f);

        Destroy(gameObject);
    }

}
