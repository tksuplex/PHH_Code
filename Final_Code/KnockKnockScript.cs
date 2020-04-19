using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockKnockScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip knock;
    float max_volume;
    PlayerScript player;

    bool scaredone;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        scaredone = false;
        max_volume = 5.0f;
        musicSource.volume = max_volume;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exit collider");
        if (!player.BeenScared && !scaredone)
        {
            if (col.gameObject.tag == "Player")
            {
                scaredone = true;
                KnockHere();

                player.BeenScared = true;
            }
        }
    }

    public void KnockHere()
    {
        musicSource.loop = false;
        musicSource.Play();
    }
}
