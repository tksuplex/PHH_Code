using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject hidemusic;
    public Button newgamebutton;
    public Button donebutton;
    public GameStateScript state;
    public GameStatusScript gs;
    public SceneChangerScript scene;

    void Start()
    {
        newgamebutton.Select();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        state = GameObject.Find("GameState").GetComponent<GameStateScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();


        // set GameState to mainmenu
        state.SetGameState(GameStateScript.GameState.MAIN_MENU);

        // set gameplay to none
        state.SetPlayState(GameStateScript.PlayState.NONE);

        // Set GameStatus to New
        gs.StartNewGame = true;

        hidemusic.SetActive(false);
    }

    public void StartNewGame()
    {
        gs.NewGame();

        state.SetGameState(GameStateScript.GameState.PLAYING_GAME);
        state.SetPlayState(GameStateScript.PlayState.PLAYER);

        // do scene manager change/fade out to new scene
        scene.FadeOutOnSceneExit(scene.fadeOutTime, 4);
    }

    public void QuitGameButton()
    {
        scene.QuitGame();
    }

    public void LoadGame()
    {
        gs.StartNewGame = false;
        state.SetGameState(GameStateScript.GameState.LOADING_GAME);

        // scene manager to load game
        scene.FadeOutOnSceneExit(scene.fadeOutTime, 1);
    }

    public void ElevatorMusic()
    {
        hidemusic.SetActive(true);
        donebutton.Select();
    }


    public void DoneElevatorMusic()
    {
        hidemusic.SetActive(false);
        newgamebutton.Select();
    }
}
