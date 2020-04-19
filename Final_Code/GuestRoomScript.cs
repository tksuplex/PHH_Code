using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRoomScript : MonoBehaviour
{
    public DialoguePopupScript dialogue;
    public string[] npcDialogue;
    _GM_Script gm;

    void Start()
    {
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !gm.gs.item.ItemCollected[33])
        {
            npcDialogue = new string[3];
            npcDialogue[0] = "Naturally, he's not here.";
            npcDialogue[1] = "His briefcase is here, and hm... '303' is written on the wall.";
            npcDialogue[2] = "Worth checking out.";
            dialogue.PopupDialogue(npcDialogue.Length, npcDialogue);
        }
    }
}
