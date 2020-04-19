using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusScript : MonoBehaviour
{
    public PlayerStatusScript player;
    public EnemyStatusScript enemy;
    public ItemStatusScript item;
    public LocationStatusScript location;

    public bool StartNewGame = true;


    // Start is called before the first frame update

    public void SaveData()
    {
        SaveLoadScript.SaveData(this);
    }

    public void LoadData()
    {
        SaveData data = SaveLoadScript.LoadData();

        player.PlayerHP = data.PlayerHP;
        player.PlayerCharges = data.PlayerCharges;
        player.WolfUnlock = data.WolfUnlock;
        player.FlashUnlock = data.FlashUnlock;
        player.BatUnlock = data.BatUnlock;
        player.RaygunUnlock = data.RaygunUnlock;
        player.BeenScared = data.BeenScared;
        player.StartGame = data.StartGame;
        player.TriggerFlash = data.TriggerFlash;
        player.MoonShineNum = data.MoonShineNum;
        player.HealthNum = data.HealthNum;

        item.ItemCollected = data.ItemCollected;

        location.PowerOn = data.PowerOn;
        location.DarkRoom = data.DarkRoom;
}

    public void NewGame()
    {
        player.PlayerMaxHP = 100;
        player.PlayerHP = player.PlayerMaxHP;
        player.PlayerCharges = 0;
        player.PlayerLoc = 4;
        player.WolfUnlock = false;
        player.FlashUnlock = false;
        player.BatUnlock = false;
        player.RaygunUnlock = false;
        player.BeenScared = false;
        player.TriggerFlash = false;
        player.StartGame = false;
        player.FlashOn = false;
        player.IsWolf = false;
        player.RayActive = false;
        player.MoonShineNum = 0;
        player.HealthNum = 0;

        location.PowerOn = true;
        location.StartLocationLights();

        item.DoNewKeyItem();
    }

    public void DeleteSave()
    {
        NewGame();

        SaveData();
    }

}
