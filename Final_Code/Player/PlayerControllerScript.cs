using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public _GM_Script gm;
    public GameObject flash;
    public GameObject wolfvision;
    public GameObject rayvision;
    public GameObject playRB;
    public Animator playerslash;

    public GameObject[] enemies;

    public PlayerScript player;
    public PlayerAttackScript attack;
    public MovePlayerScript wasd;
    public DialoguePopupScript dialogue;
    string[] dia;

    private void Start()
    {
        dialogue = GameObject.Find("DialoguePopup").GetComponent<DialoguePopupScript>();
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
    }


    public void PlayerAttack()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<EnemyScript>().inPlayerRange)
            {
                e.GetComponent<EnemyScript>().DamageEnemy(attack.damage);
            }
        }

        if (wasd.facing == MovePlayerScript.PlayerFacing.DOWN)
        {
            playerslash.SetTrigger("pswipedown");
        }
        else if (wasd.facing == MovePlayerScript.PlayerFacing.UP)
        {
            playerslash.SetTrigger("pswipeup");
        }
        else if (wasd.facing == MovePlayerScript.PlayerFacing.RIGHT)
        {
            playerslash.SetTrigger("pswiperight");
        }
        else if (wasd.facing == MovePlayerScript.PlayerFacing.LEFT)
        {
            playerslash.SetTrigger("pswipeleft");
        }



        attack.DidAttack = true;
    }

    public void PlayerDamageValueCheck()
    {
        if (player.WolfUnlock && player.IsWolf)
        {
            attack.damage = attack.WOLF_DAMAGE;
        }
        else if (player.BatUnlock)
        {
            attack.damage = attack.BAT_DAMAGE;
        }
        else
        {
            attack.damage = attack.START_DAMAGE;
        }
    }

    public void PlayerFlashCheck()
    {
        if (player.FlashUnlock && player.FlashOn)
        {
            flash.SetActive(true);
        }
        else
        {
            flash.SetActive(false);
        }
    }

    public void WolfVisionCheck()
    {
        if (player.WolfUnlock && player.IsWolf)
        {
            wolfvision.SetActive(true);
        }
        else
        {
            wolfvision.SetActive(false);
        }
    }

    public void RayVisionCheck()
    {
        if (player.RaygunUnlock && player.RayActive)
        {
            rayvision.SetActive(true);
        }
        else
        {
            rayvision.SetActive(false);
        }
    }

    public void PlayerFlashlightHandler()
    {
        if (player.FlashUnlock)
        {
            if (flash.activeInHierarchy)
            {
                flash.SetActive(false);
                player.FlashOn = false;
            }
            else if (!player.IsWolf)
            {
                flash.SetActive(true);
                player.FlashOn = true;
            }
        }
    }

    public void PlayerCycleItemHandler()
    {
        if (player.MoonShineNum <= 0 && player.HealthNum <= 0 && player.WolfUnlock == false && player.RaygunUnlock == false)
        {
            player.item = PlayerScript.PlayerItem.NONE;
        }
        else if (player.item == PlayerScript.PlayerItem.NONE)
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
        else if (player.item == PlayerScript.PlayerItem.HEALTH)
        {
            if (player.MoonShineNum >= 1)
                player.item = PlayerScript.PlayerItem.MOONSHINE;
            else if (player.WolfUnlock)
                player.item = PlayerScript.PlayerItem.MOONSTONE;
            else if (player.RaygunUnlock)
                player.item = PlayerScript.PlayerItem.RAYGUN;
            else if (player.HealthNum >= 1)
                player.item = PlayerScript.PlayerItem.HEALTH;
            else
                player.item = PlayerScript.PlayerItem.NONE;
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSHINE)
        {
            if (player.WolfUnlock)
                player.item = PlayerScript.PlayerItem.MOONSTONE;
            else if (player.RaygunUnlock)
                player.item = PlayerScript.PlayerItem.RAYGUN;
            else if (player.HealthNum >= 1)
                player.item = PlayerScript.PlayerItem.HEALTH;
            else if (player.MoonShineNum >= 1)
                player.item = PlayerScript.PlayerItem.MOONSHINE;
            else
                player.item = PlayerScript.PlayerItem.NONE;
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSTONE)
        {
            if (player.RaygunUnlock)
                player.item = PlayerScript.PlayerItem.RAYGUN;
            else if (player.HealthNum >= 1)
                player.item = PlayerScript.PlayerItem.HEALTH;
            else if (player.MoonShineNum >= 1)
                player.item = PlayerScript.PlayerItem.MOONSHINE;
            else if (player.WolfUnlock)
                player.item = PlayerScript.PlayerItem.MOONSTONE;
            else
                player.item = PlayerScript.PlayerItem.NONE;
        }
        else if (player.item == PlayerScript.PlayerItem.RAYGUN)
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
    public void PlayerItemUseHandler()
    {
        if (player.MoonShineNum <= 0 && player.HealthNum <= 0 && player.WolfUnlock == false && player.RaygunUnlock == false)
        {
            player.item = PlayerScript.PlayerItem.NONE;
        }
        else if (player.item == PlayerScript.PlayerItem.HEALTH && player.HealthNum >= 1)
        {
            player.PlayerUseHealth();
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSHINE && player.MoonShineNum >= 1)
        {
            if (player.WolfUnlock)
            {
                player.PlayerUseMoonshine();
            }
            else
            {
                dia = new string[1];
                dia[0] = "Is now really the time to be drinking moonshine?!";
                dialogue.PopupDialogue(dia.Length, dia);
            }
        }
        else if (player.item == PlayerScript.PlayerItem.MOONSTONE)
        {
            if (player.IsWolf)
            {
                player.IsWolf = false;
            }
            else
            {
                dia = new string[1];
                dia[0] = "Use the Item-Trigger button to transform! Press the Item-Trigger or Use Item button to de-transform.";
                dialogue.PopupDialogue(dia.Length, dia);
            }
        }
        else if (player.item == PlayerScript.PlayerItem.RAYGUN)
        {
            dia = new string[1];
            dia[0] = "Use the Item-Trigger button to use the Raygun! Press the Item-Trigger or Use Item button to stop.";
            dialogue.PopupDialogue(dia.Length, dia);
        }
        else
        {
            player.item = PlayerScript.PlayerItem.NONE;
        }
    }

    public void PlayerLeftTriggerHandler()
    {
        if (player.IsWolf)
        {
            player.IsWolf = false;
        }
        else if (player.RayActive)
        {
            player.RayActive = false;
        }
        else if (player.WolfUnlock && player.item == PlayerScript.PlayerItem.MOONSTONE && (player.PlayerCharges == player.PlayerMaxCharges))
        {
            player.WolfUnlock = true;
            player.IsWolf = true;
            player.PlayerCharges -= 1;
        }
        else if (player.WolfUnlock && player.item == PlayerScript.PlayerItem.MOONSTONE && (player.PlayerCharges < player.PlayerMaxCharges))
        {
            dia = new string[1];
            dia[0] = "You don't have enough power to use this item now.";
            dialogue.PopupDialogue(dia.Length, dia);
        }
        else if (player.item == PlayerScript.PlayerItem.RAYGUN && player.RaygunUnlock && !player.RayActive)
        {
            player.RayActive = true;
        }
    }

    void PlayerFlashlightOn()
    {
        flash.SetActive(true);
        player.FlashOn = true;
    }

    void PlayerFlashglightOff()
    {
        flash.SetActive(false);
        player.FlashOn = false;
    }

    void WolfVisionOn()
    {
        wolfvision.SetActive(true);
        player.IsWolf = true;
    }

    public void WolfVisionOff()
    {
        wolfvision.SetActive(false);
        player.IsWolf = false;
    }

    void RayVisionOn()
    {
        rayvision.SetActive(true);
        player.RayActive = true;
    }

    public void RayVisionOff()
    {
        rayvision.SetActive(false);
        player.RayActive = false;
    }

    private void FixedUpdate()
    {
        PlayerFlashCheck();
        WolfVisionCheck();
        RayVisionCheck();
        PlayerDamageValueCheck();
    }

}
