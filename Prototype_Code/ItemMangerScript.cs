using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMangerScript : MonoBehaviour
{
    public int HealPlayerAmount;
    public AnimationControllerScript animControl;
    public GameStatusScript gameStats;
    public ItemShutDownScript shutDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "Health" || gameObject.tag == "Health2")
        {
            animControl.PlayerBubbleAnimate("bubblehealth");
        }
        else
        {
            animControl.PlayerBubbleAnimate("bubbleq");
        }
    }
    */

    private void OnTriggerExit2D(Collider2D col)
    {
        animControl.PlayerBubbleAnimate("bubbledone");
    }

    private void OnTriggerStay2D(Collider2D col)

    {
        if (gameObject.tag == "Health" || gameObject.tag == "Health2")
        {
            animControl.PlayerBubbleAnimate("bubblehealth");
            if (Input.GetKeyDown("return"))
            {
                HealthHander();
                shutDown.RefreshItems();
            }
        }
        else if (gameObject.tag == "Briefcase")
        {
            if(!gameStats.ItemScrap)
            {
                animControl.PlayerBubbleAnimate("bubblelock");
            }
            else
            {
                animControl.PlayerBubbleAnimate("bubbleq");
                if (Input.GetKeyDown("return"))
                {
                    gameStats.ItemBriefcase = true;
                    gameStats.ItemKey2 = true;
                    shutDown.RefreshItems();
                }
            }
        }
        else
        {
            animControl.PlayerBubbleAnimate("bubbleq");
            if (Input.GetKeyDown("return"))
            {
                if (gameObject.tag == "Key")
                {
                    KeyHandler();
                    shutDown.RefreshItems();
                }
                else if (gameObject.tag == "Bat")
                {
                    BatHandler();
                    shutDown.RefreshItems();
                }
                else if (gameObject.tag == "Scrap")
                {
                    ScrapHandler();
                    shutDown.RefreshItems();
                }
                else if (gameObject.tag == "Flashlight")
                {
                    FlashlightHandler();
                    shutDown.RefreshItems();
                }
            }
        }
    }

    private void KeyHandler()
    {
        gameStats.ItemKey1 = true;
    }


    private void HealthHander()
    {
        if (gameObject.tag == "Health")
        {
            if (!gameStats.Health1)
            {
                    gameStats.Health1 = true;

                    if (gameStats.PlayerHP + HealPlayerAmount > gameStats.PlayerMaxHP)
                        gameStats.PlayerHP = gameStats.PlayerMaxHP;
                    else
                        gameStats.PlayerHP += HealPlayerAmount;
            }
        }
        else
        {
            if (!gameStats.Health2)
            {
                    gameStats.Health2 = true;

                    if (gameStats.PlayerHP + HealPlayerAmount > gameStats.PlayerMaxHP)
                        gameStats.PlayerHP = gameStats.PlayerMaxHP;
                    else
                        gameStats.PlayerHP += HealPlayerAmount;
            }

        }
    }

    private void BatHandler()
    {
        if (!gameStats.ItemBat)
        {
                gameStats.ItemBat = true;
        }
    }

    private void ScrapHandler()
    {
        if (!gameStats.ItemScrap)
        {
                gameStats.ItemScrap = true;
        }
    }

    private void FlashlightHandler()
    {
        if (!gameStats.ItemFlashlight)
        {
            if (Input.GetKeyDown("return"))
            {
                gameStats.ItemFlashlight = true;
                gameStats.FlashlightOn = true;
                animControl.FlashlightOn();
            }
        }
    }


}
