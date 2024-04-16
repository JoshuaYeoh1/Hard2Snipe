using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    BoxCollider2D coll;

    public GameObject player, teleportfx;

    public PlayerScript playerScript;

    public ScopeBehaviour scopeBehaviour;

    public TransitionScript transition;

    bool entered, stay;

    AudioManager audioManager;

    public AudioClip[] sndEnter;
    public AudioClip[] sndPoof;

    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !entered)
        {   
            StartCoroutine(teleport());
        }
    }

    void Update()
    {
        if(entered)
            playerScript.disableMove();

        if(stay)
            player.transform.position = transform.position;
    }

    IEnumerator teleport()
    {   
        entered = true;

        scopeBehaviour.holster();

        player.transform.LeanMoveLocal(new Vector2(transform.position.x,transform.position.y),1f).setEaseInOutSine();

        audioManager.playSFX(sndEnter, transform, 1f);

        yield return new WaitForSeconds(1f);

        stay = true;

        Instantiate(teleportfx,transform.position,player.transform.rotation);

        yield return new WaitForSeconds(.6f);

        player.SetActive(false);

        audioManager.playSFX(sndPoof, transform, 1f);

        yield return new WaitForSeconds(1);

        transition.RestartLevel();
    }
}
