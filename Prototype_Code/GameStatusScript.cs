using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStatusScript : MonoBehaviour
{
    public StartMenuButtonScript startmenuscript;
    public Image healthBar;

    public int PlayerHP;
    public int PlayerMaxHP;
    public int PlayerLocation;      // 0 - hall, 1- room1, 2 - room2, ...

    public int EnemyHP;
    public int EnemyMaxHP;

    public bool ItemFlashlight;
    public bool ItemBat;
    public bool ItemKey1;
    public bool ItemKey2;
    public bool ItemBriefcase;
    public bool ItemScrap;
    public bool Health1;
    public bool Health2;

    public bool FlashlightOn;
    public bool GameOver;
    public bool BeatGame;
    public bool PauseGame;

    private float HPfill;

    // Start is called before the first frame update
    void Start()
    {
        // initial setup when first loading game scene
        PlayerHP = PlayerMaxHP;
        EnemyHP = EnemyMaxHP;
        PlayerLocation = 0;

        ItemBat = false;
        ItemFlashlight = false;
        ItemKey1 = false;
        ItemKey2 = false;
        ItemBriefcase = false;
        ItemScrap = false;
        Health1 = false;
        Health2 = false;

        FlashlightOn = false;
        GameOver = false;
        BeatGame = false;
        PauseGame = false;

        Debug.Log("Player HP = " + PlayerHP);

    }

    public void HealthBarUpdate()
    {
        HPfill = (float)PlayerHP / (float)PlayerMaxHP;
        healthBar.fillAmount = HPfill;
    }

    void CheckGameOver()
    {
        if (PlayerHP <= 0)
        {
            Debug.Log("PLAYER DIED, GAME OVER!");

            if (PlayerLocation != 6)
                startmenuscript.LoadGameScene(2);
            else
                startmenuscript.LoadGameScene(3);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthBarUpdate();
        CheckGameOver();
    }
}
