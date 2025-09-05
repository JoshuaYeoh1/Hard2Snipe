using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    Animator anim;

    public GameObject player, hitboxMuzzle, hitboxPrefab;

    GameObject tempHitbox;

    bool action=true;

    Vector2 mousePos;

    public Camera cam;

    float angle;

    AudioManager audioManager;

    public AudioClip[] sndAxeSwing;
    public AudioClip[] sndAxeWindup;
    public AudioClip[] sndZombieAtk;

    void Awake()
    {
        anim = GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            swing();
        }

        pointAtMouse();
    }

    void pointAtMouse()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.x>=player.transform.position.x)
        {
            angle = Mathf.Atan2(player.transform.position.y-mousePos.y, player.transform.position.x-mousePos.x) * Mathf.Rad2Deg +180;
        }
        else if(mousePos.x<player.transform.position.x)
        {
            angle = Mathf.Atan2(player.transform.position.x-mousePos.x, player.transform.position.y-mousePos.y) * Mathf.Rad2Deg -90;
        }

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, angle);
    }

    void swing()
    {   
        if(action)
        {
            action=false;

            anim.SetTrigger("swing");

            audioManager.playSFX(sndAxeWindup, transform, 1f);
            audioManager.playSFX(sndZombieAtk, transform, 1f);
        }
        
    }

    void enableAction()
    {
        action=true;
    }

    void createHitbox()
    {
        tempHitbox = Instantiate(hitboxPrefab,hitboxMuzzle.transform.position,Quaternion.identity);

        audioManager.playSFX(sndAxeSwing, transform, 1f);
    }

    void destroyHitbox()
    {
        Destroy(tempHitbox);
    }
}
