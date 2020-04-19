using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
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

    _GM_Script gm;
    public GameStatusScript gs;
    public DialoguePopupScript dialogue;
    public PlayerScript player;
    public GameObject camerapos;
    public Animator bubble;

    public Transform warpPoint;
    public Transform alienWarp;

    public int DoorToLoc;
    public int DoorToAlienLoc;

    public bool PlayerHere;

    public bool RequiresRaygun;
    public int StrangeDrawingNo; // 4-7
    public bool DoorLocked;
    public int KeyItemNo; // 22= 202, 26 = 206, 44 = Master Key

    public string[] DoorDialogue; // Regular locked info
    public string[] AlienDialogue;

    void Start()
    {
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();


        if (DoorLocked)
        {
            DoorDialogue = new string[1];
            DoorDialogue[0] = "Locked.";
        }
        else if (DoorLocked && RequiresRaygun)
        {
            DoorDialogue = new string[1];
            DoorDialogue[0] = "Locked. There are some strange markings carved into the door.";
        }
        else if (RequiresRaygun)
        {
            AlienDialogue = new string[1];
            AlienDialogue[0] = "Jammed. The door will not open. There must be something...";
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerHere = false;
            bubble.SetTrigger("bubbledone");
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerHere = true;
            bubble.SetTrigger("bubbleq");
        }
    }
    public void DisplayDialogue(string[] dia)
    {
        if (dia.Length > 0)
        {
            dialogue.PopupDialogue(dia.Length, dia);
        }
    }

    public void InteractDoor()
    {
        if (player.RaygunUnlock && player.RayActive && RequiresRaygun)
        {
            if (gs.item.ItemCollected[StrangeDrawingNo])
            {
                if (StrangeDrawingNo == 7)
                {
                    if (gs.item.ItemCollected[4] && gs.item.ItemCollected[5] && gs.item.ItemCollected[6] && gs.item.ItemCollected[7])
                    {
                        DoAlienWarp(alienWarp.position.x, alienWarp.position.y);
                    }
                    else
                    {
                        DisplayDialogue(AlienDialogue);
                    }
                }
                else
                {
                    DoAlienWarp(alienWarp.position.x, alienWarp.position.y);
                }
            }
            else if (player.RaygunUnlock && player.RayActive)
            {
                DisplayDialogue(AlienDialogue);
            }
        }
        else
        {
            if (!DoorLocked)
            {
                DoRegularWarp(warpPoint.position.x, warpPoint.position.y);
            }
            else if (DoorLocked && gs.item.ItemCollected[KeyItemNo])
            {
                DoRegularWarp(warpPoint.position.x, warpPoint.position.y);
            }
            else if (DoorLocked && gs.item.ItemCollected[44])
            {
                DoRegularWarp(warpPoint.position.x, warpPoint.position.y);
            }
            else
            {
                DisplayDialogue(DoorDialogue);
            }

        }    
    }

    public void DoRegularWarp(float x, float y)
    {
        Vector3 temp = transform.position;
        temp.x = x;
        temp.y = y;
        player.transform.position = temp;

        player.PlayerLoc = DoorToLoc;
    }
    public void DoAlienWarp(float x, float y)
    {
        Vector3 temp = transform.position;
        temp.x = x;
        temp.y = y;
        player.transform.position = temp;

        player.PlayerLoc = DoorToAlienLoc;
    }


    private void Update()
    {
        if (PlayerHere && gm.playState == _GM_Script.PlayState.PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractDoor();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                InteractDoor();
            }
        }
    }
}
