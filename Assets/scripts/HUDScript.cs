using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    public TextMeshProUGUI ammoTextTMP, distanceTextTMP;

    public ScopeBehaviour scopeBehaviour;

    public GameObject player, teleporter, distanceArrow;

    float distance;

    void Update()
    {
        ammoTextTMP.text = scopeBehaviour.ammo.ToString();

        distance = teleporter.transform.position.x - player.transform.position.x;

        distanceTextTMP.text = ((int)distance).ToString() + "m";
    }

    void LateUpdate()
    {
        distanceArrow.transform.position = new Vector2(distanceArrow.transform.position.x,teleporter.transform.position.y);

        if(teleporter.transform.position.x-player.transform.position.x<=6)
        {
            distanceArrow.SetActive(false);
        }
        else
        {
            distanceArrow.SetActive(true);
        }
    }
}
