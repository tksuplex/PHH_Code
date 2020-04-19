using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorScript : MonoBehaviour
{
    _GM_Script gm;
    public GameStatusScript gs;
    public bool PlayerHere;
    SceneChangerScript scene;
    ElevatorScript elevator;
    public PlayerScript player;
    public Animator bubble;
    public string[] DoorDialogue; // Regular locked info
    public DialoguePopupScript dialogue;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHere = false;
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
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

    void GetOnElevator()
    {
        scene.FadeOutOnSceneExit(scene.fadeOutTime, 5);
    }

    private void Update()
    {
        if (PlayerHere && gm.playState == _GM_Script.PlayState.PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (scene.SceneIndex != 5)
                {
                    GetOnElevator();
                }
                else
                {
                    elevator = GameObject.Find("Elevator").GetComponent<ElevatorScript>();
                    elevator.GetCurrentFloor();
                    elevator.ExitToFloor();
                }
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                if (scene.SceneIndex != 5)
                {
                    GetOnElevator();
                }
                else
                {
                    elevator = GameObject.Find("Elevator").GetComponent<ElevatorScript>();
                    elevator.GetCurrentFloor();
                    elevator.ExitToFloor();
                }
            }
        }
    }
}
