using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibsScript : MonoBehaviour
{

    Rigidbody2D rb;
    CircleCollider2D coll;
    AudioManager audioManager;

    public AudioClip[] sndGibs;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

        float angle=Random.Range(135f,45f);

        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        float force=Random.Range(500f,700f);

        rb.AddForce(dir*force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        audioManager.playSFX(sndGibs, transform, 1f);
    }
}
