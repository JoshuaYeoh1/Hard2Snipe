using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    GameObject player;
    PlayerScript playerScript;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

        StartCoroutine(killSelf());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            playerScript.playerShot();
        }
        Destroy(gameObject);
    }

    IEnumerator killSelf()
    {
        yield return new WaitForSeconds(.1f);

        Destroy(gameObject);
    }
    
}
