using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerItem {  NONE, HEALTH, MOONSHINE, MOONSTONE, RAYGUN }
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

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerHP <= 0)
        {
            PlayerHP = 1;
        }
/*
        WolfUnlock = false;
        FlashUnlock = false;
        BatUnlock = false;
        TriggerFlash = false;
        RaygunUnlock = false;
        BeenScared = false;
        StartGame = true;
*/
    }

    public void UpdateItemEquip()
    {
        if (HealthNum <= 0 && MoonShineNum <= 0 && WolfUnlock == false && RaygunUnlock == false)
        {
            item = PlayerItem.NONE;
        }
        else if (item == PlayerItem.HEALTH && HealthNum <= 0)
        {
            if (MoonShineNum >= 1)
                item = PlayerItem.MOONSHINE;
            else if (WolfUnlock)
                item = PlayerItem.MOONSTONE;
            else if (RaygunUnlock)
                item = PlayerItem.RAYGUN;
        }
        else if (item == PlayerItem.MOONSHINE && MoonShineNum <= 0)
        {
            if (HealthNum >= 1)
                item = PlayerItem.HEALTH;
            else if (WolfUnlock)
                item = PlayerItem.MOONSTONE;
            else if (RaygunUnlock)
                item = PlayerItem.RAYGUN;
        }
        else if (item == PlayerItem.MOONSTONE)
        {
            if (HealthNum >= 1)
                item = PlayerItem.HEALTH;
            else if (MoonShineNum >= 1)
                item = PlayerItem.MOONSHINE;
            else if (RaygunUnlock)
                item = PlayerItem.RAYGUN;
        }
        else if (item == PlayerItem.RAYGUN)
        {
            if (HealthNum >= 1)
                item = PlayerItem.HEALTH;
            else if (MoonShineNum >= 1)
                item = PlayerItem.MOONSHINE;
            else if (WolfUnlock)
                item = PlayerItem.MOONSTONE;
        }
    }

    public void PlayerUseHealth()
    {
        HealthNum -= 1;
        UpdateItemEquip();

        if ((PlayerMaxHP - PlayerHP) > 65)
        {
            PlayerHP += 65;
        }
        else
        {
            PlayerHP = PlayerMaxHP;
        }
    }

    public void PlayerUseMoonshine()
    {
        MoonShineNum -= 1;
        UpdateItemEquip();
        PlayerCharges = PlayerMaxCharges;
    }
}
