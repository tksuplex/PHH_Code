using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyItemsMenuScript : MonoBehaviour
{
    public TextMeshProUGUI keyleft;
    public TextMeshProUGUI keyright;
    public TextMeshProUGUI fixcontrol;

    _GM_Script gm;
    ItemStatusScript item;
    bool[] ItemCollected;
    string leftmsg;
    string rightmsg;
    string leftcontrols;

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

    void Start()
    {
        leftmsg = "";
        rightmsg = "";
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        item = GameObject.Find("ItemStatus").GetComponent<ItemStatusScript>();
        leftcontrols = "WASD / DPAD / Joysticks \n'E' / [A] / [CROSS]\n'Q' / [B] / [CIRCLE] \n";
        leftcontrols += "'M' / [Y] / [TRIANGLE] \n'I' / [X] / [SQUARE] \n'F' / [RB] / [R1] \n";
        leftcontrols += "TAB / [LB] / [L1] \nSPACE / [RT] / [R2] \n'T' or SHIFT / [LT] / [L2]\n\nESC";
        fixcontrol.text = leftcontrols;
    }

    public void DisplayKeyItems()
    {
        leftmsg = "";
        rightmsg = "";
        ItemCollected = item.ItemCollected;

        if (gm.player.HealthNum > 0)
            leftmsg += "Health Pack (" + gm.player.HealthNum + ")\n";
        else
            leftmsg += "\n";
        if (gm.player.MoonShineNum > 0)
            leftmsg += "Moonshine (" + gm.player.MoonShineNum + ")\n";
        else
            leftmsg += "\n";
        if (ItemCollected[0])
            rightmsg += "Flashlight\n";
        else
            leftmsg += "\n";
        if (ItemCollected[1])
            rightmsg += "Baseball Bat\n";
        else
            leftmsg += "\n";
        if (ItemCollected[2])
            leftmsg += "Moonstone\n";
        else
            leftmsg += "\n";
        if (ItemCollected[3])
            leftmsg += "Raygun\n";
        else
            leftmsg += "\n";

        if (!ItemCollected[3])
        {
            if (ItemCollected[8])
                rightmsg += "Weird Junk 1\n";
            else
                rightmsg += "\n";
            if (ItemCollected[9])
                rightmsg += "Weird Junk 2\n";
            else
                rightmsg += "\n";
            if (ItemCollected[10])
                rightmsg += "Weird Junk 3\n";
            else
                rightmsg += "\n";
        }

        // ---------------------------// 

        if (!ItemCollected[44])
        {
            if (ItemCollected[22])
                rightmsg += "Room 202 Key\n";
            else
                rightmsg += "\n";
            if (ItemCollected[26])
                rightmsg += "Room 206 Key\n";
            else
                rightmsg += "\n";
            if (ItemCollected[33])
                rightmsg += "Room 303 Key\n";
            else
                rightmsg += "\n";
            if (ItemCollected[11])
                rightmsg += "Room 101 Key\n";
            else
                rightmsg += "\n";
        }

        if (ItemCollected[44])
            rightmsg += "Master Key\n";
        else
            rightmsg += "\n";

        if (ItemCollected[4])
            rightmsg += "Strange Drawing 1\n";
        else
            rightmsg += "\n";
        if (ItemCollected[5])
            rightmsg += "Strange Drawing 2\n";
        else
            rightmsg += "\n";
        if (ItemCollected[6])
            rightmsg += "Strange Drawing 3\n";
        else
            rightmsg += "\n";
        if (ItemCollected[7])
            rightmsg += "Strange Drawing 4\n";
        else
            rightmsg += "\n";


        keyleft.text = leftmsg;
        keyright.text = rightmsg;
    }

    private void FixedUpdate()
    {
        DisplayKeyItems();
    }

}
