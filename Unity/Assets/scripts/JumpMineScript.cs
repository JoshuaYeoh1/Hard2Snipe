using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMineScript : MonoBehaviour
{
    GibbedScript gibbedScript;

    PlayerScript playerScript;

    GameObject player;

    public GameObject parent;

    public int force;

    AudioManager audioManager;

    public AudioClip[] sndBoost;
    public AudioClip[] sndBoost2;
    public AudioClip[] sndExplode;

    void Awake()
    {
        gibbedScript = parent.GetComponent<GibbedScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            if(player.transform.position.x>=transform.position.x)
                playerScript.mineJump(force,-1);
            else
                playerScript.mineJump(force,1);

            gibbedScript.gibbed();

            audioManager.playSFX(sndBoost, transform, 1f);
            audioManager.playSFX(sndBoost2, transform, 1f);
            audioManager.playSFX(sndExplode, transform, 1f);

            Destroy(parent.gameObject);
        }
    }


}
