using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBehaviour : MonoBehaviour
{
    
    public GameObject player, scope, bullet, scopeout, muzzle, flash;
    
    public float aimTime, recoilSpeed, recoilAmount;

    float aimX, aimY;

    public int maxAmmo=5;

    public int ammo;

    public bool aimScope;

    bool reloading;

    GameObject explosive;

    AudioManager audioManager;

    public AudioClip[] sndM700;
    public AudioClip[] sndReloading;
    public AudioClip[] sndLoadBullet;
    public AudioClip[] sndReloaded;
    public AudioClip[] sndAim;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        
        StartCoroutine(startingPos());

        ammo=maxAmmo;
        //Debug.Log("ammo is "+ammo);
    }

    void Update()
    {
        if(explosive != null)
        {
            aimX = explosive.transform.position.x;
            aimY = explosive.transform.position.y;
        }
        else
        {
            aimX = player.transform.position.x;
            aimY = player.transform.position.y;
        }
        
        if(aimScope)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(aimX,aimY), aimTime*Time.deltaTime);
        }

        if(reloading)
        {
            transform.position = scopeout.transform.position;
        }
    }

    IEnumerator shooting()
    {
        while(ammo>0)
        {
            yield return new WaitForSeconds(Random.Range(2f,3.5f));
            
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        audioManager.playSFX(sndM700, transform, 1f);

        Instantiate(bullet,scope.transform.position,scope.transform.rotation);

        Instantiate(flash,muzzle.transform.position,muzzle.transform.rotation);

        ammo--;
        //Debug.Log("ammo is "+ammo);

        aimScope = false;

        yield return null;

        transform.LeanMoveLocal(new Vector2(transform.position.x,transform.position.y+recoilAmount),recoilSpeed).setEaseOutSine();

        yield return new WaitForSeconds(recoilSpeed);

        transform.LeanMoveLocal(new Vector2(transform.position.x,transform.position.y-recoilAmount),recoilSpeed*10).setEaseOutBounce();

        yield return new WaitForSeconds(Random.Range(.5f,1f));

        if(ammo>0)
        {
            aimScope = true;

            audioManager.playSFX(sndAim, transform, 1f);
        }
        else
        {
            StartCoroutine(reload());
        }
    }

    IEnumerator reload()
    {
        StopCoroutine(shooting());

        aimScope = false;

        transform.LeanMoveLocal(scopeout.transform.position,.75f).setEaseInOutSine();

        audioManager.playSFX(sndReloading, transform, 1f);

        yield return new WaitForSeconds(.75f);

        reloading=true;

        yield return new WaitForSeconds(Random.Range(0.5f,1f));

        while(ammo != maxAmmo)
        {
            ammo++;
            //Debug.Log("ammo is "+ammo);

            audioManager.playSFX(sndLoadBullet, transform, 1f);

            yield return new WaitForSeconds(Random.Range(.5f,1f));
        }

        if(ammo == maxAmmo)
        {
            StartCoroutine(drawGun());
        }
        
    }

    IEnumerator drawGun()
    {
        reloading=false;

        transform.LeanMoveLocal(new Vector2(aimX,aimY),.75f).setEaseInOutSine();

        audioManager.playSFX(sndReloaded, transform, 1f);

        yield return new WaitForSeconds(.75f);

        aimScope = true;

        audioManager.playSFX(sndAim, transform, 1f);

        StartCoroutine(shooting());
    }

    IEnumerator startingPos()
    {
        reloading=true;

        yield return new WaitForSeconds(4);

        StartCoroutine(drawGun());
    }

    public void holster()
    {
        StopAllCoroutines();

        aimScope = false;

        transform.LeanMoveLocal(scopeout.transform.position,.75f).setEaseInOutSine();
    }

    public void aimExplosive(GameObject _explosive)
    {
        explosive = _explosive;
    }
}
