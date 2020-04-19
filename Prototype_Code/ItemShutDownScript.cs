using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShutDownScript : MonoBehaviour
{
    public GameStatusScript gameStats;
    public AnimationControllerScript animControl;
    public GameObject Key1;
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Briefcase;
    public GameObject Bat;
    public GameObject Flashlight;
    public GameObject Scrap;

    public void RefreshItems()
    {
        if (gameStats.ItemKey1)
        {
            Key1.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.ItemBriefcase)
        {
            Briefcase.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.Health1)
        {
            Health1.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.Health2)
        {
            Health2.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.ItemBat)
        {
            Bat.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.ItemFlashlight)
        {
            Flashlight.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
        if (gameStats.ItemScrap)
        {
            Scrap.SetActive(false);
            animControl.PlayerBubbleAnimate("bubbledone");
        }
    }
}
