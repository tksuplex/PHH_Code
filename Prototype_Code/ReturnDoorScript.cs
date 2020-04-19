using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnDoorScript : MonoBehaviour
{
    public GameStatusScript gameStatus;
    public AnimationControllerScript AnimControl;
    public BeamMeUpScript beam;

    // Start is called before the first frame update
    void Start()
    {

    }

/*
        if (gameObject.tag == "Enemy")
        {
            Debug.Log("THIS OBJECT IS TAGGED 'ENEMY'");
        }

*/

private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if ((gameStatus.PlayerLocation == 3 || gameStatus.PlayerLocation == 4 || gameStatus.PlayerLocation == 6)
                && !gameStatus.FlashlightOn)
            {
                AnimControl.PlayerBubbleAnimate("bubbledot");
            }
            else if (gameStatus.PlayerLocation == 0)
            {
                if ((gameObject.tag == "Door3" || gameObject.tag == "Door6") && gameStatus.FlashlightOn)
                {
                    if (gameObject.tag == "Door6" && !gameStatus.ItemKey2)
                    {
                        // shit's locked
                        AnimControl.PlayerBubbleAnimate("bubblelock");
                    }
                    else
                    {
                        AnimControl.PlayerBubbleAnimate("bubbledoor");

                        if (Input.GetKeyDown("return"))
                        {
                            if (gameObject.tag == "Door6")
                                beam.TeleportRoom(6);
                            else
                                beam.TeleportRoom(3);
                        }
                    }
                }
                else if ((gameObject.tag == "Door3" || gameObject.tag == "Door6") && !gameStatus.FlashlightOn)
                {
                    // shit's dark
                }
                else if (gameObject.tag == "Door4" && !gameStatus.ItemKey1)
                {
                    // shit's locked
                    AnimControl.PlayerBubbleAnimate("bubblelock");
                }
                else
                {
                    AnimControl.PlayerBubbleAnimate("bubbledoor");

                    if (Input.GetKeyDown("return"))
                    {
                        if (gameObject.tag == "Door1")
                            beam.TeleportRoom(1);
                        else if (gameObject.tag == "Door2")
                            beam.TeleportRoom(2);
                        else if (gameObject.tag == "Door4")
                            beam.TeleportRoom(4);
                        else
                            beam.TeleportRoom(5);
                    }
                }
            }
            else
            {
                AnimControl.PlayerBubbleAnimate("bubbledoor");

                if (Input.GetKeyDown("return"))
                {
                    // rocket back to hallway
                    beam.TeleportRoom(0);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if ((gameStatus.PlayerLocation == 3 || gameStatus.PlayerLocation == 4 || gameStatus.PlayerLocation == 6)
            && !gameStatus.FlashlightOn)
        {
            AnimControl.PlayerBubbleAnimate("bubbledot");
        }
        else
        {
            // animate none bubble
            AnimControl.PlayerBubbleAnimate("bubbledone");
        }
    }

}
