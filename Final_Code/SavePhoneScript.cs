using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePhoneScript : MonoBehaviour
{
    _GM_Script gm;
    public GameObject hidethis;
    public Button topbutton;
    public Animator bubble;
    public bool PlayerHere;

    public GameStatusScript gs;
    public PlayerScript player;
    public LocationControllerScript loc;
    public SceneChangerScript scene;
    public int count;
    public bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        waiting = false;
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        loc = GameObject.Find("LocationController").GetComponent<LocationControllerScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
        bubble = GameObject.Find("PlayerBubble").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();

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
            PlayerHere = true;
            bubble.SetTrigger("bubbleq");
        }
    }

    public void OpenPhoneMenu()
    {
        Debug.Log("Open Phone Menu");
        gm.playState = _GM_Script.PlayState.MENU;
        gm.state.playState = GameStateScript.PlayState.MENU;
        hidethis.SetActive(true);
        topbutton.Select();
    }

    public void SaveGame()
    {
        Debug.Log("Saving Game");
        gm.gameState = _GM_Script.GameState.SAVING_GAME;
        gm.state.gameState = GameStateScript.GameState.SAVING_GAME;

        hidethis.SetActive(false);
        waiting = true;
        scene.FadeOutOnSceneExit(scene.fadeOutTime, 1);
    }

    public void LoadGame()
    {
        Debug.Log("Loading Game");
        gm.gameState = _GM_Script.GameState.LOADING_GAME;
        gm.state.gameState = GameStateScript.GameState.LOADING_GAME;
        

        hidethis.SetActive(false);
        waiting = true;
        scene.FadeOutOnSceneExit(scene.fadeOutTime, 1);
    }

    public void DeleteSave()
    {
        Debug.Log("Deleting Save");
        gs.DeleteSave();

        hidethis.SetActive(false);
        waiting = true;
        gm.playState = _GM_Script.PlayState.PLAYER;
    }

    public void ExitMenu()
    {
        hidethis.SetActive(false);
        waiting = true;
        gm.playState = _GM_Script.PlayState.PLAYER;
    }


    private void Update()
    {
        if (PlayerHere && gm.playState == _GM_Script.PlayState.PLAYER && !waiting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenPhoneMenu();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                OpenPhoneMenu();
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
