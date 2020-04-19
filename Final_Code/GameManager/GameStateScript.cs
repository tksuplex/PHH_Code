using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour
{
    public enum GameState { MAIN_MENU, LOADING_GAME, SAVING_GAME, PLAYING_GAME, GAME_OVER, BEAT_GAME }
    public enum PlayState { NONE, WAITING, PLAYER, MENU, DIALOGUE }

    public GameState gameState;
    public PlayState playState;

    // Should be starting in Main Menu before playing
    void Start()
    {
        gameState = GameState.MAIN_MENU;
        playState = PlayState.NONE;
    }

    // Set Game State from input
    public void SetGameState(GameState gs)
    {
        gameState = gs;
    }

    // Set Play State from input
    public void SetPlayState(PlayState ps)
    {
        playState = ps;
    }

    // Set State without input example
    public void SetGameStateMenu()
    {
        gameState = GameState.MAIN_MENU;
    }

    // call from another script;
    // gsScript.TestSetState(GameStateScript.GameState.SAVING_GAME);
    public void TestSetState(GameState gs)
    {
        gameState = gs;
    }

/*
    private void FixedUpdate()
    {
        if (scene.buildIndex == 0)
        {
            gameState = GameState.MAIN_MENU;
        }
        else if (scene.buildIndex == 4 || scene.buildIndex == 5 || scene.buildIndex == 6 || 
            scene.buildIndex == 7 || scene.buildIndex == 8 || scene.buildIndex == 9 || 
            scene.buildIndex == 10 || scene.buildIndex == 11)
        {
            gameState = GameState.PLAYING_GAME;
        }
    }
*/
}
