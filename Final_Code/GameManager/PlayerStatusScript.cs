using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    /* Item ID:
     * 0-9 EQUIPMENT
     *      0 none
     *      1 Flashlight
     *      2 Bat
     *      3 Moonstone
     *      4 Raygun
     */

    public enum PlayerItem { NONE, HEALTH, MOONSHINE, MOONSTONE, RAYGUN }
    public PlayerItem item;

    public int PlayerHP;
    public int PlayerMaxHP;
    public int PlayerCharges;
    public int PlayerMaxCharges;
    public int PlayerLoc = 0;

    public bool WolfUnlock = false;
    public bool FlashUnlock = false;
    public bool BatUnlock = false;
    public bool RaygunUnlock = false;

    public bool FlashOn = false;
    public bool IsWolf = false;
    public bool RayActive = false;

    public bool BeenScared = false;
    public bool TriggerFlash = false;
    public bool StartGame = true;

    public int MoonShineNum;
    public int HealthNum;

    void Start()
    {
        PlayerCharges = 0;
        PlayerMaxCharges = 10;
        PlayerMaxHP = 100;
        PlayerHP = PlayerMaxHP;
        PlayerCharges = PlayerMaxCharges;
        item = PlayerItem.NONE;
        BeenScared = false;
        TriggerFlash = false;
        WolfUnlock = false;
        BatUnlock = false;
        RaygunUnlock = false;
        FlashUnlock = false;
        StartGame = true;
    }

}
