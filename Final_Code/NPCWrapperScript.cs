using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWrapperScript : MonoBehaviour
{
    public GameObject hidenpc;
    public NPCScript npc;

    void Start()
    {
        //npc = GameObject.Find("NPCWrapper").GetComponent<NPCScript>();
    }

    public IEnumerator StartNPC()
    {
        yield return new WaitForSeconds(0.1f);

        if (!npc.npcUnlocked && npc.npcDoneInteraction)
        {
            hidenpc.SetActive(false);
        }
        else
        {
            hidenpc.SetActive(true);
        }
    }
}