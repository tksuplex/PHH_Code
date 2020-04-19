using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public AudioClip menumusic;
    public AudioClip countdown;
    public AudioClip gluttony;
    public AudioClip room;
    public AudioClip collageman;
    public AudioClip gingerly;
    public AudioClip mediumshot;
    public AudioClip youarethemoveyoumake;

    public float max_volume;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        max_volume = 0.3f;


        musicSource.volume = max_volume;

        checkSceneBGM();

    }

    public void checkSceneBGM()
    {
        bool fadeIn = true;
        bool loop = true;
        AudioClip clippy = null;
        float fadeInTime = 0;

        if (musicSource.isPlaying)
        {
            clippy = musicSource.clip;
        }

        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0 || scene.buildIndex == 2)
        {
            // menu music
            loop = true;
            fadeIn = true;
            fadeInTime = 1.0f;
            clippy = menumusic;
            checkCurrentBGM(clippy, loop, fadeIn, fadeInTime);
        }
        else if (scene.buildIndex == 7 || scene.buildIndex == 8 || scene.buildIndex == 9)
        {
            loop = true;
            fadeIn = true;
            fadeInTime = 1.0f;

            int rand = Random.Range(0, 7);

            if (rand == 0)
                clippy = countdown;
            else if (rand == 1)
                clippy = gluttony;
            else if (rand == 2)
                clippy = room;
            else if (rand == 3)
                clippy = collageman;
            else if (rand == 4)
                clippy = gingerly;
            else if (rand == 5)
                clippy = mediumshot;
            else if (rand == 6)
                clippy = youarethemoveyoumake;
            else if (rand == 7)
                clippy = null;

            if (clippy != null)
                checkCurrentBGM(clippy, loop, fadeIn, fadeInTime);
        }
        else
        {
            FadeOutBGM(1.0f);
        }

    }


    void checkCurrentBGM(AudioClip bgm, bool loop, bool fadeIn, float fadeInTime)
    {
        if (musicSource.isPlaying)
        {
            if (musicSource.clip != bgm) // change to correct BGM
            {
                StartBGM(bgm, loop, fadeIn, fadeInTime);
            }
        }
        else // not playing anything
        {
            StartBGM(bgm, loop, fadeIn, fadeInTime);
        }
    }

    public void StartBGM(AudioClip bgm, bool loop, bool fadeIn, float fadeInTime)
    {
        StopBGM();
        musicSource.loop = loop;
        musicSource.clip = bgm;
        if (fadeIn)
        {
            FadeInBGM(fadeInTime);
        }
        else
        {
            musicSource.volume = max_volume;
            musicSource.Play();
        }
    }


    public void FadeOutBGM(float time)
    {
        if (musicSource.isPlaying)
        {
            StartCoroutine(FadeOut(musicSource, time));
        }
    }

    public void FadeInBGM(float time)
    {
        StartCoroutine(FadeIn(musicSource, time));
    }

    // https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }


    // https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < max_volume)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void StopBGM()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

}
