using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatusScript : MonoBehaviour
{
    /*
     * Item ID:
     * 
     *      0 Flashlight
     *      1 Bat
     *      2 Moonstone
     *      3 Raygun
     *      
     *      4 Strange Drawing 1
     *      5 Strange Drawing 2
     *      6 Strange Drawing 3
     *      7 Strange Drawing 4
     *      
     *      8 Weird Junk 1
     *      9 Weird Junk 2
     *      10 Weird Junk 3
     *      
     *      11 Room 101 Key
     *      22 Room 202 Key
     *      26 Room 206 Key
     *      33 Room 303 Key
     *      
     *      44 Master Key
     *      
     *      45 Health 1
     *      46 Health 2
     *      47 Health 3
     *      48 Health 4
     *      49 Health 5
     *      
     *      50 Moonshine 1
     *      51 Moonshine 2
     *      52 Moonshine 3
     *      53 Moonshine 4
     *      54 Moonshine 5
     *      
     */

    public int HealthNum;
    public int MoonshineNum;

    // Save this
    public bool[] ItemCollected;
    public int TOTAL_ITEMS_NUM;

    // Start is called before the first frame update
    void Start()
    {
        DoNewKeyItem();
    }

    public void DoNewKeyItem()
    {
        TOTAL_ITEMS_NUM = 60;
        ItemCollected = new bool[TOTAL_ITEMS_NUM];

        for (int i = 0; i < TOTAL_ITEMS_NUM; i++)
        {
            ItemCollected[i] = false;
        }
    }
}
