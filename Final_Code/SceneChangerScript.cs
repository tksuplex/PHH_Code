using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    // public AudioManagerScript audi;
    public Animator blackfade;
    public Animator blackfadeout;
    public GameObject blackScreen;
    public GameObject blackScreen2;
    public MusicScript music;

    public float fadeInTime;
    public float fadeOutTime;
    Scene scene;

    public int SceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<MusicScript>();
        music.checkSceneBGM();

        scene = SceneManager.GetActiveScene();
        SceneIndex = scene.buildIndex;

        blackScreen = GameObject.Find("BlackFade");
        blackfade = blackScreen.GetComponent<Animator>();
        blackScreen2 = GameObject.Find("BlackFadeOut");
        blackfadeout = blackScreen2.GetComponent<Animator>();
        blackScreen.SetActive(true);
        blackScreen2.SetActive(false);

        FadeInOnSceneStart(fadeInTime);

        if(SceneIndex == 2) // GAME OVER
        {
            StartCoroutine(WaitToFadeOutScene(4.0f, 0));  // fade out, back to main menu
        }
        else if (SceneIndex == 3) // Beat game
        {
            StartCoroutine(WaitToFadeOutScene(5.0f, 0)); // fade out, back to mmenu
        }
    }


    public void TestTransition(float time)
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            FadeOutOnSceneExit(time, 8);
        }
    }

    public void FadeInOnSceneStart(float time)
    {
        this.StartCoroutine(FadeIn(time));
    }


    public IEnumerator FadeIn(float time)
    {
        yield return new WaitForSeconds(time);
        blackScreen.SetActive(false);
    }

    public void CallWaitFadeOut(float time, int scene)
    {
        StartCoroutine(WaitToFadeOutScene(time, scene));  // fade out, back to main menu
    }

    public IEnumerator WaitToFadeOutScene(float time, int scene)
    {
        yield return new WaitForSeconds(time);
        FadeOutOnSceneExit(fadeOutTime, scene);
    }

    public void FadeOutOnSceneExit(float time, int scene)
    {
        blackScreen2.SetActive(true);
        blackfadeout.SetTrigger("fadeout");

        this.StartCoroutine(FadeOut(time, scene));
    }

    public IEnumerator FadeOut(float time, int scene)
    {
        yield return new WaitForSeconds(time);
        LoadSceneNum(scene);
    }


    public void LoadSceneNum(int num)
    {
        Debug.Log("Loading Scene Number " + num);
        SceneManager.LoadScene(num);
    }

    public void QuitGame()
    {
        Debug.Log("NOW EXITING GAME...");
        Application.Quit();
    }

}