using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteScript : MonoBehaviour
{
    public PlayerScript playerScript;

    void footstep()
    {
        playerScript.footstep();
    }
}
