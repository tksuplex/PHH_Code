using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    _GM_Script gm;
    public GameStatusScript gs;
    public SceneChangerScript scene;
    public DialoguePopupScript dialogue;
    public PlayerScript player;
    public Animator bubble;

    public bool PlayerHere;
    public bool FoundAlien;

    public string npcName;

    public int npcID;
    public bool npcUnlocked;
    public bool npcDoneInteraction;

    public string[] npcDialogue;
    public int[] giveItemID;
    public bool giveHealth;
    public bool giveMoonshine;

    public bool npcAlienOnly;

    void Start()
    {
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
        this.StartCoroutine(StartNPC());
    }

    public IEnumerator StartNPC()
    {
        yield return new WaitForSeconds(0.1f);

        /*
        npcDoneInteraction = gs.npc.npcDoneInteraction[npcID];
        npcUnlocked = gs.npc.npcUnlocked[npcID];
        */

        this.name = "NPC_" + npcID + "_" + npcName;
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
            if (npcAlienOnly)
            {
                if (player.RayActive && !player.FlashOn)
                {
                    bubble.SetTrigger("bubbleq");
                }
                else
                {
                    bubble.SetTrigger("bubbledone");
                }
            }
            else
            {
                bubble.SetTrigger("bubbleq");
            }
        }
    }

    public void DisplayDialogue()
    {
        if (npcDialogue.Length > 0)
        {
            dialogue.PopupDialogue(npcDialogue.Length, npcDialogue);
        }
    }

    public void GiveItemsToPlayer()
    {
        if (giveItemID.Length > 0)
        {
            for (int i = 0; i < giveItemID.Length; i++)
            {
                gs.item.ItemCollected[giveItemID[i]] = true;

            }

        }
        if (giveHealth)
        {
            player.HealthNum += 1;
        }
        if (giveMoonshine)
        {
            player.MoonShineNum += 1;
        }
    }

    public void InteractNPC()
    {
        if (npcAlienOnly)
        {
            if (player.RayActive && !player.FlashOn)
            {
                if (npcID == 44)
                {
                    npcDialogue = new string[1];
                    npcDialogue[0] = "Well, you found me. Now what?";
                    DisplayDialogue();

                    scene.CallWaitFadeOut(1.0f, 3);
                }
                GiveItemsToPlayer();
                DisplayDialogue();
            }
        }
        else if (npcID == 0) // manager
        {
            if (player.HealthNum <= 0 && gs.item.ItemCollected[0])
            {
                player.HealthNum += 1;
                npcDialogue = new string[2];
                npcDialogue[0] = "I hope this will help you.";
                npcDialogue[1] = "< You have recieved a Health Pack! >";
                DisplayDialogue();
            }
            else if (gs.item.ItemCollected[33])
            {
                npcDialogue = new string[1];
                npcDialogue[0] = "Any luck yet?";
                DisplayDialogue();
            }
            else if (gs.item.ItemCollected[0])
            {
                gs.item.ItemCollected[33] = true;
                npcDialogue = new string[2];
                npcDialogue[0] = "You have a lead? Oh, you need the key to Room 303? Well, alright...";
                npcDialogue[1] = "< You have recieved the key to Room 303! >";
                DisplayDialogue();
            }
            else if (gs.item.ItemCollected[22])
            {
                npcDialogue = new string[1];
                npcDialogue[0] = "Let me know if you need anything else...";
                DisplayDialogue();
            }
            else
            {
                gs.item.ItemCollected[22] = true;
                gs.item.ItemCollected[26] = true;
                npcDialogue = new string[6];
                npcDialogue[0] = "Oh, Detective, thank goodness you're here.";
                npcDialogue[1] = "As I said on the phone we have an urgent need for your skills.";
                npcDialogue[2] = "One of our guests has gone missing, and we suspect foul play. " +
                    "These strange, violent men have arrived and harassing our staff looking for the missing guest.";
                npcDialogue[3] = "Look, there's one over there.";
                npcDialogue[4] = "Take these keys. I've prepared a room for you on the second floor, Room 202. " +
                    "The missing guest's room number is 206";
                npcDialogue[5] = "< You have recieved the keys to Room 202 and 206! >";
                DisplayDialogue();
            }
        }
        else if (npcID == 1) // elevator
        {
            if (player.MoonShineNum <= 0 && gs.item.ItemCollected[0])
            {
                player.MoonShineNum += 1;
                npcDialogue = new string[2];
                npcDialogue[0] = "Care to wet your whistle?";
                npcDialogue[1] = "< You have recieved a jar of Moonshine! >";
                DisplayDialogue();
            }
            else if (gs.item.ItemCollected[0])
            {
                npcDialogue = new string[2];
                npcDialogue[0] = "We're having power outages on top of everything else? I need a new job.";
                npcDialogue[1] = "They're going to make me try to fix the defaced door on the first floor I just know it.";
                DisplayDialogue();
            }
            else if (!gs.item.ItemCollected[0] && player.HealthNum <= 0)
            {
                player.HealthNum += 1;
                npcDialogue = new string[5];
                npcDialogue[0] = "It's awful about that guest that went missing... they told you about it right?";
                npcDialogue[1] = "So many weird people staying here lately.";
                npcDialogue[2] = "Er, I'm sure you're perfectly normal, Sir.";
                npcDialogue[3] = "Here take this and don't tell my boss, eh?";
                npcDialogue[4] = "< You recieved a health pack! >";
                DisplayDialogue();
            }
        }
        else if (npcID == 2) // scientist
        {
            if (gs.item.ItemCollected[8] && gs.item.ItemCollected[9] && gs.item.ItemCollected[10])
            {
                if (gs.item.ItemCollected[4])
                {
                    npcDialogue = new string[1];
                    npcDialogue[0] = "Tell me about the weapon, for science. I'm surprised you haven't vaporized yourself yet.";
                    DisplayDialogue();
                }
                else
                {
                    gs.item.ItemCollected[3] = true;
                    npcDialogue = new string[3];
                    npcDialogue[0] = "What interesting things you've brought me... let me see...";
                    npcDialogue[1] = "Look you can assemble it, like so. Hm. Not sure of it's effectiveness. Why don't you test it out for me?";
                    npcDialogue[2] = "< You have recieved a Raygun! >";
                    DisplayDialogue();
                }
            }
            else if (gs.item.ItemCollected[8] || gs.item.ItemCollected[9] || gs.item.ItemCollected[10])
            {
                npcDialogue = new string[2];
                npcDialogue[0] = "What interesting things you've brought me... let me see...";
                npcDialogue[1] = "If you find any more WEIRD things bring them here...";
                DisplayDialogue();
            }
            else
            {
                npcDialogue = new string[2];
                npcDialogue[0] = "What have you got there... let me see...";
                npcDialogue[1] = "Nothing interesting... Go away!";
                DisplayDialogue();
            }
        }
        /*
                else
                {
                    if (!npcDoneInteraction)
                    {
                        GiveItemsToPlayer();
                        npcDoneInteraction = true;
                        DisplayDialogue();
                    }
                }
            }

            public void UpdateNPC()
            {
                gs.npc.npcDoneInteraction[npcID] = npcDoneInteraction;
            }
        */

    }

    private void Update()
    {
        if (PlayerHere && gm.playState == _GM_Script.PlayState.PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractNPC();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                InteractNPC();
            }
        }
    }
}
