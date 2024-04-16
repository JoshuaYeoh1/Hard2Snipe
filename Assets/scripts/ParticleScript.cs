using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem movementParticle;

    public ParticleSystem fallParticle;

    [Range(0,10)]
    public int occurAfterVelocity;

    [Range(0,.2f)]
    public float dustFormationPeriod;

    public Rigidbody2D playerRb;

    float counter;

    bool isGrounded;
    
    AudioManager audioManager;

    public AudioClip[] sndLand;

    public GameObject anchor;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        counter += Time.deltaTime;

        if(isGrounded && Mathf.Abs(playerRb.velocity.x)>occurAfterVelocity)
        {
            if(counter>dustFormationPeriod)
            {
                movementParticle.Play();

                counter=0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ground"))
        {
            isGrounded=true;

            fallParticle.Play();

            audioManager.playSFX(sndLand, transform, 1f);

            SquashStretch();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("ground"))
        {
            isGrounded=false;
        }
    }

    void SquashStretch()
    {
        StartCoroutine(SquashStretchAnim());
    }

    IEnumerator SquashStretchAnim()
    {
        float squashvalue = 0.3f;

        anchor.transform.LeanScale(new Vector2(transform.localScale.x+squashvalue,transform.localScale.y-squashvalue),.1f).setEaseInSine();

        yield return new WaitForSeconds(.1f);        

        anchor.transform.LeanScale(new Vector2(transform.localScale.x,transform.localScale.y),.1f).setEaseOutSine();
    }
}
