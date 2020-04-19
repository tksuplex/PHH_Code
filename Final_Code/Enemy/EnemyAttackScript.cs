using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public float TimeBetween;
    public bool playerInRange;
    public int ENEMY_DAMAGE;

    private float TimeCountdown;
    void Start()
    {
        playerInRange = false;
    }


    public bool EnemyCanAttack()
    {
        if (TimeCountdown <= 0)
        {
            TimeCountdown = TimeBetween;
            return true;
        }
        else
        {
            TimeCountdown -= Time.deltaTime;
            return false;
        }
    }

    public void EnemyAttackPlayer()
    {
        if (TimeCountdown <= 0)
        {


            TimeCountdown = TimeBetween;
        }
        else
        {
            TimeCountdown -= Time.deltaTime;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerInRange)
            playerInRange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerInRange)
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }
}
