using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class PlayerHUDScript : MonoBehaviour
{
    public Image hpbar;
    public Image chargebar;
    public Image item;
    public Image havebat;
    public Image haveflash;

    public TextMeshProUGUI itemtext;

    public Sprite none;
    public Sprite health;
    public Sprite moonshine;
    public Sprite moonstone;
    public Sprite raygun;
    public Sprite bat;
    public Sprite flashlight;

    public GameObject redscreen;

    PlayerScript player;
    float HPpercent;
    float ChargePercent;

    string itemnum;
    bool setonstart;
    int count;

    private void Start()
    {
        count = 0;
        setonstart = false;
        itemnum = "";
        HPpercent = 0;
        ChargePercent = 0;
        redscreen.SetActive(false);

        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }


    private void FixedUpdate()
    {
        if (player.PlayerHP <= 30)
        {
            redscreen.SetActive(true);
        }
        else
        {
            redscreen.SetActive(false);
        }

        if (!setonstart && count >= 20)
        {
            setonstart = true;
            if (player.MoonShineNum <= 0 && player.HealthNum <= 0 && player.WolfUnlock == false && player.RaygunUnlock == false)
            {
                player.item = PlayerScript.PlayerItem.NONE;
            }
            else
            {
                if (player.HealthNum >= 1)
                    player.item = PlayerScript.PlayerItem.HEALTH;
                else if (player.MoonShineNum >= 1)
                    player.item = PlayerScript.PlayerItem.MOONSHINE;
                else if (player.WolfUnlock)
                    player.item = PlayerScript.PlayerItem.MOONSTONE;
                else if (player.RaygunUnlock)
                    player.item = PlayerScript.PlayerItem.RAYGUN;
                else
                    player.item = PlayerScript.PlayerItem.NONE;
            }
        }
        else
        {
            count++;
        }

        HPpercent = (float)player.PlayerHP / (float)player.PlayerMaxHP;
        hpbar.fillAmount = HPpercent;
        chargebar.fillAmount = ChargePercent;

        if (player.PlayerHP >= 0 && player.PlayerHP <= player.PlayerMaxHP)
            hpbar.fillAmount = HPpercent;

        ChargePercent = (float)player.PlayerCharges / (float)player.PlayerMaxCharges;
        if (player.PlayerCharges >= 0 && player.PlayerCharges <= player.PlayerMaxCharges)
            chargebar.fillAmount = ChargePercent;

        if (player.item == PlayerScript.PlayerItem.NONE)
        {
            item.overrideSprite = none;
            itemnum = "";
            itemtext.text = itemnum;
        }
        else if (player.item == PlayerScript.PlayerItem.HEALTH)
        {
            item.overrideSprite = health;
            itemnum = "(";
            itemnum += player.HealthNum;
            itemnum += ")";
            itemtext.text = itemnum;
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSHINE)
        {
            item.overrideSprite = moonshine;
            itemnum = "(";
            itemnum += player.MoonShineNum;
            itemnum += ")";
            itemtext.text = itemnum;
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSTONE)
        {
            item.overrideSprite = moonstone;
            itemnum = "";
            itemtext.text = itemnum;
        }
        else if (player.item == PlayerScript.PlayerItem.RAYGUN)
        {
            item.overrideSprite = raygun;
            itemnum = "";
            itemtext.text = itemnum;
        }

        count++;

        if (player.FlashUnlock)
        {
            haveflash.overrideSprite = flashlight;
            haveflash.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            haveflash.color = new Color(1f, 1f, 1f, 0f);
        }

        if (player.BatUnlock)
        {
            havebat.overrideSprite = bat;
            havebat.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            havebat.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
