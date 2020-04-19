using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoLoadSaveScript : MonoBehaviour
{
    public GameStateScript state;
    public GameStatusScript gs;
    public SceneChangerScript scene;
    int numsave;

    void Start()
    {
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        state = GameObject.Find("GameState").GetComponent<GameStateScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();


        StartCoroutine(CheckSaveLoad());
    }


    public IEnumerator CheckSaveLoad()
    {
        yield return new WaitForSeconds(1f);

        if (state.gameState == GameStateScript.GameState.LOADING_GAME)
        {
            // Do loading Game
            LoadGame();
        }
        else if (state.gameState == GameStateScript.GameState.SAVING_GAME)
        {
            // Do saving game
            SaveGame();
        }
    }

    public void LoadGame()
    {
        numsave = PlayerPrefs.GetInt("NumSave", 0);
        if (numsave == 0)
        {
            gs.NewGame();
            PlayerPrefs.Save();
            Debug.Log("No Saves To Load");
        }
        else
        {
            gs.LoadData();
            Debug.Log("Loading Save");
        }

        scene.FadeOutOnSceneExit(scene.fadeOutTime, 4);
    }

    public void SaveGame()
    {
        gs.SaveData();
        numsave = 1;
        PlayerPrefs.SetInt("NumSave", numsave);
        PlayerPrefs.Save();
        Debug.Log("Saving Game");

        scene.FadeOutOnSceneExit(scene.fadeOutTime, 4);
    }
}
