using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Utilized this tutorial: https://www.youtube.com/watch?v=XOjd_qU2Ido

[System.Serializable]
public class SaveData
{
    // From Player
    public int PlayerHP;
    public int PlayerCharges;

    public bool WolfUnlock;
    public bool FlashUnlock;
    public bool BatUnlock;
    public bool RaygunUnlock;

    public bool BeenScared;
    public bool TriggerFlash;
    public bool StartGame;

    public int MoonShineNum;
    public int HealthNum;

    public bool[] ItemCollected;

    public bool PowerOn;
    public bool[] DarkRoom;



    public SaveData(GameStatusScript gs)
    {
        PlayerHP = gs.player.PlayerHP;
        PlayerCharges = gs.player.PlayerCharges;
        WolfUnlock = gs.player.WolfUnlock;
        FlashUnlock = gs.player.FlashUnlock;
        BatUnlock = gs.player.BatUnlock;
        RaygunUnlock = gs.player.RaygunUnlock;
        BeenScared = gs.player.BeenScared;
        TriggerFlash = gs.player.TriggerFlash;
        MoonShineNum = gs.player.MoonShineNum;
        HealthNum = gs.player.HealthNum;
        StartGame = gs.player.StartGame;

        ItemCollected = gs.item.ItemCollected;

        PowerOn = gs.location.PowerOn;
        DarkRoom = gs.location.DarkRoom;
    }
}
