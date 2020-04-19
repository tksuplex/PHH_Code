using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class LocationStatusScript : MonoBehaviour
{
    /*
     * LOCATION GUIDE:
     * 
     * 0 - none
     * 1 - lobby
     * 2 - elevator
     * 3 - pool
     * 4 - basement
     * 5 - roof
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
     */

    public enum CurrentFloor { NONE, LOBBY, BASEMENT, ROOF, FLOOR1, FLOOR2, FLOOR3, UNKNOWN }
    public CurrentFloor floor;

    public int LocationTotal;

    // save this
    public bool[] DarkRoom;
    public bool PowerOn;

    // Start is called before the first frame update
    void Start()
    {
        PowerOn = true;
        LocationTotal = 80;
        floor = CurrentFloor.NONE;

        StartLocationLights();
    }

    public void StartLocationLights()
    {
        DarkRoom = new bool[LocationTotal];

        for (int i = 0; i < LocationTotal; i++)
        {
            DarkRoom[i] = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
