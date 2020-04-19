using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomScript : MonoBehaviour
{
    public _GM_Script state;
    public SceneChangerScript scene;
    public LocationControllerScript loc;

    void Start()
    {
        state = GameObject.Find("_GM").GetComponent<_GM_Script>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
        loc = GameObject.Find("LocationController").GetComponent<LocationControllerScript>();

        StartCoroutine(CheckSaveLoad());
    }

    public IEnumerator CheckSaveLoad()
    {
        yield return new WaitForSeconds(0.5f);

        // check if condition met for player to turn power / on off
        if (loc.PowerOn)
        {
            // enable buttong to turn power off
        }

        if (state.gameState == _GM_Script.GameState.LOADING_GAME)
        {
            state.gameState = _GM_Script.GameState.PLAYING_GAME;
        }
        else if (state.gameState == _GM_Script.GameState.SAVING_GAME)
        {
            state.gameState = _GM_Script.GameState.PLAYING_GAME;
        }
    }

    public void SaveGame()
    {
        state.gameState = _GM_Script.GameState.SAVING_GAME;

        scene.FadeOutOnSceneExit(scene.fadeOutTime, 1);
    }

}
