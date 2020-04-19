using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    public float AttackRestTime;
    private float AttackRestRemaining;
    public GameStatusScript gameStatus;
    public AnimationControllerScript AnimControl;
    public bool inEnemyRange;
    public int EnemyAttackValue;

    // Start is called before the first frame update
    void Start()
    {
        inEnemyRange = false;
    }

    public void EnemyAttack(string dir)
    {
        if (AttackRestRemaining <= 0)
        {
            if (inEnemyRange)
            {
                EnemyAttackFacing(dir);
                AnimControl.ShakeCamera();
                AnimControl.PlayerBubbleAnimate("bubbleskull");
                gameStatus.PlayerHP -= EnemyAttackValue;
            }
            AttackRestRemaining = AttackRestTime;
        }
        else
        {
            AttackRestRemaining -= Time.deltaTime;
        }
    }

    void EnemyAttackFacing(string dir)
    {
        if (dir == "left")
            AnimControl.EnemyAttackImpact("impactleft");
        else if (dir == "right")
            AnimControl.EnemyAttackImpact("impactright");
        else if (dir == "down")
            AnimControl.EnemyAttackImpact("impactdown");
        else if (dir == "up")
            AnimControl.EnemyAttackImpact("impactup");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !inEnemyRange)
        {
            inEnemyRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        inEnemyRange = false;
    }
}
