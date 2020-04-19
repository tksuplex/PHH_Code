using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationControllerScript : MonoBehaviour
{
    /*
     * LOCATION GUIDE:
     * 
     * 0 - none
     * 4 - lobby
     * 5 - elevator
     * 6 - safe room
     * 
     * 7 - floor1 hallway
     * 8 - floor2 hallway
     * 9 - floor3 hallway
     * 
     * 11 - floor1 room1
     * 12 - floor1 room2
     * 13 - floor1 room3
     * 14 - floor1 room4
     * 15 - floor1 room5
     * 16 - floor1 room6
     * 
     * 21 - floor2 room1
     * 22 - floor2 room2
     * 23 - floor2 room3
     * 24 - floor2 room4
     * 25 - floor2 room5
     * 26 - floor2 room6
     * 
     * 31 - floor3 room1
     * 32 - floor3 room2
     * 33 - floor3 room3
     * 34 - floor3 room4
     * 35 - floor3 room5
     * 36 - floor3 room6
     * 
     * 44 - Alien Room
     */

    public enum CurrentFloor { NONE, LOBBY, BASEMENT, ROOF, FLOOR1, FLOOR2, FLOOR3, UNKNOWN }
    public CurrentFloor floor;

    public PlayerScript player;

    public bool[] DarkRoom;
    public int LocationTotal;
    public bool PowerOn;

    Scene scene;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();


        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0)
        {
            floor = CurrentFloor.NONE;
        }
        else if (scene.buildIndex == 4)
        {
            Debug.Log("CURRENT LOCATION SHOULD BE 1");
            floor = CurrentFloor.LOBBY;
            player.PlayerLoc = 4;
        }
        else if (scene.buildIndex == 6)
        {
            // THIS IS THE SAFE ROOM FLOOR
            floor = CurrentFloor.FLOOR2;
            player.PlayerLoc = 6;
        }
        else if (scene.buildIndex == 5) // elevator 
        {
            player.PlayerLoc = 5;
        }
        else if (scene.buildIndex == 7)
        {
            floor = CurrentFloor.FLOOR1;
            player.PlayerLoc = 7;
        }
        else if (scene.buildIndex == 8)
        {
            floor = CurrentFloor.FLOOR2;
            player.PlayerLoc = 8;
        }
        else if (scene.buildIndex == 9)
        {
            floor = CurrentFloor.FLOOR3;
            player.PlayerLoc = 9;
        }
        /*
                else if (scene.buildIndex == 10)
                {
                    floor = CurrentFloor.BASEMENT;
                    player.PlayerLoc = 10;
                }
                else if (scene.buildIndex == 11)
                {
                    floor = CurrentFloor.ROOF;
                }
        */
        else if (scene.buildIndex == 2)
        {
            floor = CurrentFloor.NONE;
        }
        else if (scene.buildIndex == 3)
        {
            floor = CurrentFloor.NONE;
        }

    }

    public void CheckIfSwitchLights()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 7 || scene.buildIndex == 8 || scene.buildIndex == 9)
        {
            int rand = Random.Range(0, 9);

            if (!player.WolfUnlock && rand <= 7)
            {
                Debug.Log("turn power off");
                TurnPowerOff();
            }
            else if (player.FlashUnlock && rand <= 4)
            {
                Debug.Log("turn power off");
                TurnPowerOff();
            }
            else
            {
                TurnPowerBackOn();
            }
        }
    }

    public void TurnPowerOff()
    {
        PowerOn = false;
        for (int i = 0; i < LocationTotal; i++)
        {
            DarkRoom[i] = true;
        }
    }

    public void TurnPowerBackOn()
    {
        PowerOn = true;
        for (int i = 0; i < LocationTotal; i++)
        {
            // May want exceptions for certain rooms
            DarkRoom[i] = false;
        }
    }

}
