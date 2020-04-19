using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public bool inPlayerRange;
    public int playerAttackValue;
    public GameStatusScript gameStatus;
    public AnimationControllerScript AnimControl;

    public float AttackRestTime;
    public float AttackRestRemaining;


    // Start is called before the first frame update
    void Start()
    {
        inPlayerRange = false;        
    }

    public void PlayerAttack(string dir)
    {
        if (AttackRestRemaining <= 0)
        {
            PlayerAttackFacing(dir);
            if (inPlayerRange && gameStatus.EnemyHP > 0)
            {
                AnimControl.ShakeCamera();
                AnimControl.EnemyGetsHurt();
                gameStatus.EnemyHP -= playerAttackValue;
            }
            AttackRestRemaining = AttackRestTime;
        }
        else
        {
            AttackRestRemaining -= Time.deltaTime;
        }
    }


    void PlayerAttackFacing(string dir)
    {
        if (dir == "left")
            AnimControl.PlayerSlashAnimate("pswipeleft");
        else if (dir == "right")
            AnimControl.PlayerSlashAnimate("pswiperight");
        else if (dir == "down")
            AnimControl.PlayerSlashAnimate("pswipedown");
        else if (dir == "up")
            AnimControl.PlayerSlashAnimate("pswipeup");
    }


    public void PlayerAttack(float x, float y)
    {
        if (AttackRestRemaining <= 0)
        {
            if (inPlayerRange)
            {
                PlayerAttackFacing(x, y);
                AnimControl.ShakeCamera();
                AnimControl.EnemyGetsHurt();
                gameStatus.EnemyHP -= playerAttackValue;
            }
            AttackRestRemaining = AttackRestTime;
        }
        else
        {
            AttackRestRemaining -= Time.deltaTime;
        }
    }

    void PlayerAttackFacing(float x, float y)
    {
        if (x != 0)
        {
            if (x < 0)
            {
                AnimControl.PlayerSlashAnimate("pswipeleft");
            }
            else
            {
                AnimControl.PlayerSlashAnimate("pswiperight");
            }
        }
        else if (y != 0)
        {
            if (y < 0)
            {
                AnimControl.PlayerSlashAnimate("pswipedown");
            }
            else
            {
                AnimControl.PlayerSlashAnimate("pswipeup");
            }
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !inPlayerRange)
        {
            inPlayerRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        inPlayerRange = false;
    }
}
