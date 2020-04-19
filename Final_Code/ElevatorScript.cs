using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour
{
    _GM_Script gm;
    public LocationControllerScript loc;
    public SceneChangerScript scene;
    public int FLOOR_EXIT_SCENE;
    public GameObject hidethis;
    public Button lobby;
    public Animator bubble;
    public bool PlayerHere;
    public GameStatusScript gs;
    public PlayerScript player;
    public string[] DoorDialogue; // Regular locked info
    public DialoguePopupScript dialogue;
    public int count;
    public bool waiting;

    private void Start()
    {
        count = 0;
        waiting = false;
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        loc = GameObject.Find("LocationController").GetComponent<LocationControllerScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();

        GetCurrentFloor();
        player.PlayerLoc = 5;

        hidethis.SetActive(false);
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
            if (DarkAtLocation() && (!player.FlashUnlock || !player.FlashOn))
            {
                DoorDialogue = new string[1];
                DoorDialogue[0] = "Too dark. Can't see the buttons.";
                dialogue.PopupDialogue(DoorDialogue.Length, DoorDialogue);
                PlayerHere = false;
            }
            else
            {
                PlayerHere = true;
                bubble.SetTrigger("bubbleq");
            }
        }
    }


    public bool DarkAtLocation()
    {
        if (gs.location.DarkRoom[player.PlayerLoc])
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void GetCurrentFloor()
    {
        // SCENES: 4 lobby, 5 elevator, 7 floor 1, 8 floor 2, 9 floor 3, 10 basement
        if (loc.floor == LocationControllerScript.CurrentFloor.LOBBY)
        {
            FLOOR_EXIT_SCENE = 4;
        }
        else if (loc.floor == LocationControllerScript.CurrentFloor.FLOOR1)
        {
            FLOOR_EXIT_SCENE = 7;
        }
        else if (loc.floor == LocationControllerScript.CurrentFloor.FLOOR2)
        {
            FLOOR_EXIT_SCENE = 8;
        }
        else if (loc.floor == LocationControllerScript.CurrentFloor.FLOOR3)
        {
            FLOOR_EXIT_SCENE = 9;
        }
/*
        else if (loc.floor == LocationControllerScript.CurrentFloor.BASEMENT)
        {
            FLOOR_EXIT_SCENE = 10;
        }
*/
    }

    public void OpenElevatorMenu()
    {
        gm.playState = _GM_Script.PlayState.MENU;
        hidethis.SetActive(true);
        lobby.Select();
    }


    public void ElevatorToFloor(int requested)
    {
        hidethis.SetActive(false);
        waiting = true;
        gm.playState = _GM_Script.PlayState.PLAYER;

        if (requested == 4)
        {
            Debug.Log("YOU ARE REQUESTING LOBBY");
            loc.floor = LocationControllerScript.CurrentFloor.LOBBY;
        }
        else if (requested == 7)
        {
            Debug.Log("YOU ARE REQUESTING FLOOR1");
            loc.floor = LocationControllerScript.CurrentFloor.FLOOR1;
        }
        else if (requested == 8)
        {
            Debug.Log("YOU ARE REQUESTING FLOOR2");
            loc.floor = LocationControllerScript.CurrentFloor.FLOOR2;
        }
        else if (requested == 9)
        {
            Debug.Log("YOU ARE REQUESTING FLOOE3");
            loc.floor = LocationControllerScript.CurrentFloor.FLOOR3;
        }
/*
        else if (requested == 10)
        {
            Debug.Log("YOU ARE REQUESTING BASEMENT");
            loc.floor = LocationControllerScript.CurrentFloor.BASEMENT;
        }
*/
        GetCurrentFloor();
    }

    public void ExitToFloor()
    {
        scene.FadeOutOnSceneExit(scene.fadeOutTime, FLOOR_EXIT_SCENE);
    }


    private void Update()
    {
        if (PlayerHere && gm.playState == _GM_Script.PlayState.PLAYER && !waiting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenElevatorMenu();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                OpenElevatorMenu();
            }
        }
        else if (count < 25)
        {
            count++;
        }
        else
        {
            waiting = false;
            count = 0;
        }
    }











}
