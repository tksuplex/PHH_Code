using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatusScript : MonoBehaviour
{
    // Save this
    public bool[] npcUnlocked;
    public bool[] npcDoneInteraction;
    public int TOTAL_ITEMS_NUM;

    // Start is called before the first frame update
    void Start()
    {
        TOTAL_ITEMS_NUM = 100;
        npcUnlocked = new bool[TOTAL_ITEMS_NUM];
        npcDoneInteraction = new bool[TOTAL_ITEMS_NUM];

        for (int i = 0; i < TOTAL_ITEMS_NUM; i++)
        {
            npcDoneInteraction[i] = false;
            npcUnlocked[i] = true;
        }
    }
}
