using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtonScript : MonoBehaviour
{
    //  Function to exit the game (called by button)
    public void StartMenuQuitGame()
    {
        Debug.Log("EXITING THE GAME.");
        Application.Quit();
    }

    // Function to start playing the game from the menu (called by button)
    public void StartMenuPlayGame(int num)
    {
        Debug.Log("NEW GAME IS STARTING.");
        SceneManager.LoadScene(num);
    }

    public void LoadGameScene(int num)
    {
        Debug.Log("LOADING SCENE [" + num + "]...");
        SceneManager.LoadScene(num);
    }
}
