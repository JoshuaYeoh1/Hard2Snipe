using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour
{
    Animator anim;

    public int numberOfSkins=1;

    void Awake()
    {
        anim = GetComponent<Animator>();

        int skin = Random.Range(1,numberOfSkins+1);
        anim.SetTrigger(skin.ToString()); 
    }
}
