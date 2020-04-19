using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_TESTING_SCRIPT : MonoBehaviour
{
    GameObject player;
    GameObject[] enemies;
    _GM_Script gm;

    public bool LIGHT_ON;
    public bool BAT_UNLOCK;
    public bool WOLF_UNLOCK;
    public bool RAYGUN_UNLOCK;

    public int HEALTHITEM_NUM;
    public int MOONSHINE_NUM;

    private bool SET_PLAYSTATE;
    void Start()
    {
        LIGHT_ON = false;
        SET_PLAYSTATE = false;
        player = GameObject.Find("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        gm.playState = _GM_Script.PlayState.PLAYER;
    }

    // Update is called once per frame
    void Update()
    {
//        gm.gs.DeleteSave();
/*
        if (LIGHT_ON)
            gm.location.PowerOn = true;
        else
            gm.location.PowerOn = false;

        if (player.GetComponent<PlayerScript>().PlayerHP <= 30)
        {
            player.GetComponent<PlayerScript>().PlayerHP += 20;
        }
        player.GetComponent<PlayerScript>().FlashUnlock = true;
//        player.GetComponent<PlayerScript>().BatUnlock = true; // BAT_UNLOCK;
//        player.GetComponent<PlayerScript>().WolfUnlock = WOLF_UNLOCK;
        player.GetComponent<PlayerScript>().RaygunUnlock = RAYGUN_UNLOCK;
        player.GetComponent<PlayerScript>().MoonShineNum = MOONSHINE_NUM;
        player.GetComponent<PlayerScript>().HealthNum = HEALTHITEM_NUM;
        // gm.gs.item.ItemCollected[0] = true;
//        gm.gs.item.ItemCollected[2] = true;
//        WOLF_UNLOCK = player.GetComponent<PlayerScript>().WolfUnlock;

        */

//        gm.aggro = _GM_Script.AggroProgress.WATCHING;
//        gm.gameState = _GM_Script.GameState.PLAYING_GAME;
/*
        if (!SET_PLAYSTATE)
        {
            if (gm.playState == _GM_Script.PlayState.NONE)
                gm.playState = _GM_Script.PlayState.PLAYER;
            SET_PLAYSTATE = true;
        }
*/        
    }
}
