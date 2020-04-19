using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlashlightScript : MonoBehaviour
{
    PlayerScript player;
    _GM_Script gm;
    bool triggerdone;

    // Start is called before the first frame update
    void Start()
    {
        triggerdone = false;
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!player.TriggerFlash && !triggerdone)
        {
            if (col.gameObject.tag == "Player")
            {
                triggerdone = true;

                gm.location.PowerOn = false;

                player.TriggerFlash = true;
            }
        }
    }
}
