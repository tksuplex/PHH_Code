using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemScript : MonoBehaviour
{
    _GM_Script gm;
    public PlayerScript player;
    public GameStatusScript gs;
    public DialoguePopupScript dialogue;
    public Animator bubble;

    public bool PlayerHere;

    public int ItemID;
    public string ItemName;
    public bool ItemCollected;
    public bool ItemWolfOnly;
    public bool ItemAlienOnly;
    public bool ItemInDarkness;
    public bool isHealth;
    public bool isMoonshine;

    public string[] ItemDialogue;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();


        this.StartCoroutine(StartItem());
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
            if (ItemCollected == false)
            {
                if (ItemWolfOnly)
                {
                    if (player.IsWolf && !player.FlashOn)
                    {
                        bubble.SetTrigger("bubbleq");
                    }
                }
                else if (ItemAlienOnly)
                {
                    if (player.RayActive && !player.FlashOn)
                    {
                        bubble.SetTrigger("bubbleq");
                    }
                }
                else if (ItemInDarkness)
                {
                    if (gm.player.FlashOn)
                    {
                        bubble.SetTrigger("bubbleq");
                    }
                }
                else
                {
                    bubble.SetTrigger("bubbleq");
                }
            }

        }
    }


    public void DisplayDialogue()
    {
        if (ItemDialogue.Length > 0)
        {
            dialogue.PopupDialogue(ItemDialogue.Length, ItemDialogue);
        }
    }

    public void PlayerItemInteract()
    {

        // add check bool for game progress unlocked

        if (ItemCollected == false)
        {
            if (ItemWolfOnly)
            {
                if (player.IsWolf && !player.FlashOn)
                {
                    PickUpItem();
                }
            }
            else if (ItemAlienOnly)
            {
                if (player.RayActive && !player.FlashOn)
                {
                    PickUpItem();
                }
            }
            else if (ItemInDarkness)
            {
                if (player.FlashOn)
                {
                    PickUpItem();
                }
            }
            else
            {
                PickUpItem();
            }
        }
        else
        {
            gs.item.ItemCollected[ItemID] = true;
            Debug.Log("THERE HAS BEEN AN ERROR THIS ITEM WAS COLLECTED ALREADY.");
            // delete this item
            Destroy(gameObject);
        }
    }

    void PickUpItem()
    {
        ItemCollected = true;
        gs.item.ItemCollected[ItemID] = true;

        if(isHealth)
            player.HealthNum += 1;
        else if (isMoonshine)
            player.MoonShineNum += 1;

        UpdateItem();

        DisplayDialogue();

        Destroy(gameObject);
    }

    public void CheckPickedUp()
    {
        if (ItemCollected)
        {
            PickUpItem();
        }
    }

    public IEnumerator GetItem()
    {
        yield return new WaitForSeconds(0.1f);

        int i = ItemID;
        ItemCollected = gs.item.ItemCollected[i];
        this.name = "Item_" + i + "_" + ItemName;
    }

    public IEnumerator StartItem()
    {
        yield return new WaitForSeconds(0.1f);

        int i = ItemID;
        ItemCollected = gs.item.ItemCollected[i];
        this.name = "Item_" + i + "_" + ItemName;
    }

    public void UpdateItem()
    {
        int i = ItemID;
        gs.item.ItemCollected[i] = ItemCollected;
    }

    private void Update()
    {
        if (gs.item.ItemCollected[ItemID])
        {
            Destroy(gameObject);
        }
        if (PlayerHere && gm.playState ==  _GM_Script.PlayState.PLAYER)
        {
            CheckPickedUp();

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerItemInteract();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                PlayerItemInteract();
            }
        }
    }
}

