using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;

    BoxCollider2D coll;

    Animator anim;

    public ScopeBehaviour scopeBehaviour;

    AudioManager audioManager;

    public float speed, jumpPower;
    
    float dirX;

    public LayerMask groundLayer;

    //[SerializeField]
    bool iframe, faceR=true, move=true, passedDeathPit, jumpable=true;

    Vector2 mousePos;

    public Camera cam;

    GibbedScript gibbedScript;

    public TransitionScript transition;

    public GameObject dmgvig, deathPit, anchor, sprite;

    int coverCount;

    public AudioClip[] sndFootsteps;
    public AudioClip[] sndFleshExplode;
    public AudioClip[] sndSpike;
    public AudioClip[] sndJump;
    public AudioClip[] sndSubwoofer;
    public AudioClip[] sndZombieDie;
    public AudioClip[] sndZombieJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        coll = GetComponent<BoxCollider2D>();

        anim = sprite.GetComponent<Animator>();

        gibbedScript = GetComponent<GibbedScript>();

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

        dmgvig.SetActive(false);
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if(move)
        {
            rb.velocity = new Vector2(dirX*speed, rb.velocity.y);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if(mousePos.x<transform.position.x && faceR)
        {
            flip();
        }
        else if(mousePos.x>transform.position.x && !faceR)
        {
            flip();
        }

        if(dirX==0)
        {
            anim.SetBool("running",false);

            anim.SetBool("runningback",false);
        }
        else if((dirX>0 && mousePos.x>transform.position.x) || (dirX<0 && mousePos.x<transform.position.x))
        {
            anim.SetBool("running",true);

            anim.SetBool("runningback",false);
        }
        else if((dirX>0 && mousePos.x<transform.position.x) || (dirX<0 && mousePos.x>transform.position.x))
        {
            anim.SetBool("running",false);

            anim.SetBool("runningback",true);
        }

        if(Input.GetButtonDown("Jump") && isGrounded() && move)
        {
            StartCoroutine(jump());
        }

        anim.SetBool("grounded",isGrounded());

        if(coverCount>0)
        {
            iframe = true;
        }
        else
        {
            iframe = false;
        }

        if(!passedDeathPit && transform.position.y<deathPit.transform.position.y)
        {
            passedDeathPit=true;

            playerDie();
        }
    }

    IEnumerator jump()
    {
        if(jumpable)
        {
            SquashStretch();

            jumpable=false;

            yield return new WaitForSeconds(.1f);

            jumpable=true;

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            audioManager.playSFX(sndJump, transform, 1f);

            audioManager.playSFX(sndZombieJump, transform, 1f);
        }
    }

    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,0.1f,groundLayer);
    }

    void flip()
    {   
        transform.Rotate(0,180,0);
        faceR = !faceR;
    }

    public void playerShot()
    {
        if(iframe)
        {
            Debug.Log("Player Protected");
        }
        else
        {
            playerDie();
        }
    }

    public void playerDie()
    {
        gibbedScript.gibbed();

        audioManager.playSFX(sndZombieDie, transform, 1f);

        audioManager.playSFX(sndFleshExplode, transform, 1f);

        audioManager.playSFX(sndSubwoofer, transform, 1f);

        scopeBehaviour.holster();

        dmgvig.SetActive(true);

        transition.RestartLevelDelay();

        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "spike" && !isGrounded())
        {   
            audioManager.playSFX(sndSpike, transform, 1f);

            playerDie();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "cover")
        {
            coverCount++;
        }

        if(other.gameObject.tag == "explosion")
        {
            playerDie();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "cover")
        {
            coverCount--;
        }
    }

    public void disableMove()
    {
        move = false;
    }

    public void footstep()
    {
        if(dirX!=0 && isGrounded())
        {
            audioManager.playSFX(sndFootsteps, transform, 1f); 
        }
    }

    public void mineJump(int force, int dir)
    {
        StartCoroutine(tempStopMove());

        rb.velocity = Vector2.zero;

        rb.velocity = new Vector2(force*dir,force*2);
    }

    IEnumerator tempStopMove()
    {
        move=false;

        yield return new WaitForSeconds(.5f);

        move=true;
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

        anchor.transform.LeanScale(new Vector2(transform.localScale.x-squashvalue,transform.localScale.y+squashvalue),.1f).setEaseOutSine();

        yield return new WaitForSeconds(.1f);        

        anchor.transform.LeanScale(new Vector2(transform.localScale.x,transform.localScale.y),.5f).setEaseOutSine();
    }
}
