using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEditor.U2D.Path.GUIFramework;

public class GameMenuScript : MonoBehaviour,
    IEventSystemHandler
//    ISelectHandler
//    IDeselectHandler,
//    IPointerDownHandler,
//    IUpdateSelectedHandler
{
    public enum MenuState { MAIN, KEYITEM, CONTROLS }
    public MenuState state;

    public SceneChangerScript scene;
    public KeyItemsMenuScript key;
    public GameObject menu;
    public GameObject gameMenu;
    public GameObject itemMenu;
    public GameObject controlMenu;

    public Button keyBtn;
    public Button controlBtn;
    public Button quitBtn;
    public Button exitBtn;

    public Button returnItem;
    public Button returnControl;

    public Button btn;
    public _GM_Script gm;

    void Start()
    {
        state = MenuState.MAIN;
        menu.SetActive(false);
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
    }

    public void OpenMenuHandler()
    {
        gm.playState = _GM_Script.PlayState.MENU;
        Debug.Log("print state: " + gm.playState);
        menu.SetActive(true);
        gameMenu.SetActive(true);
        itemMenu.SetActive(false);
        controlMenu.SetActive(false);
        keyBtn.Select();
    }

    public void CancelMenuHandler()
    {
        if (state == MenuState.MAIN)
        {
            // if in main, exit out
            menu.SetActive(false);
            gm.playState = _GM_Script.PlayState.PLAYER;
        }
        else
        {
            // if in equip return to main
            gameMenu.SetActive(true);
            itemMenu.SetActive(false);
            controlMenu.SetActive(false);
            state = MenuState.MAIN;
            keyBtn.Select();
        }
    }

    public void DirectionalMenuHandler()
    {
        // This is already handled by the system
    }

    public void SelectMenuHandler(int button)
    {
        if (button == 0)                // key item
        {
            state = MenuState.KEYITEM;
            itemMenu.SetActive(true);
            gameMenu.SetActive(false);
            returnItem.Select();
            key.DisplayKeyItems();
        }
        else if (button == 1)           // controls
        {
            state = MenuState.CONTROLS;
            controlMenu.SetActive(true);
            gameMenu.SetActive(false);
            returnControl.Select();

        }
        else if (button == 2)           // quit game
        {
            // quit game
            scene.FadeOutOnSceneExit(scene.fadeOutTime, 0);
        }
        else if (button == 3)        // exit menu
        {
            CancelMenuHandler();
        }
        else if (button == 4)     // return from item
        {
            CancelMenuHandler();
        }
        else if (button == 5)  // return from controls
        {
            CancelMenuHandler();
        }
    }

    public void SelectMenuHandler()
    {
        if (btn == keyBtn)
        {
            itemMenu.SetActive(true);
            gameMenu.SetActive(false);
            returnItem.Select();
            key.DisplayKeyItems();
        }
        else if (btn == controlBtn)
        {
            controlMenu.SetActive(true);
            gameMenu.SetActive(false);
            returnControl.Select();

        }
        else if (btn == quitBtn)
        {
            // quit game
            
        }
        else if (btn == exitBtn)
        {
            CancelMenuHandler();
        }
        else if (btn == returnItem)
        {
            CancelMenuHandler();
        }
        else if (btn == returnControl)
        {
            CancelMenuHandler();
        }
    }

    /*
    void Update()
    {
        if (gm.gameState == _GM_Script.GameState.PLAYING_GAME && gm.playState == _GM_Script.PlayState.MENU)
            menu.SetActive(true);
        else
            menu.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Button btn2 = GetComponent<Button>();
        btn = GetComponent<Button>();
        Debug.Log("BUTTON SELECTED: " + this.gameObject.name);
    }
    */



}
