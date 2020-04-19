using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float TimeBetween;
    private float TimeRemaining;
    public bool CanAttack;
    public bool DidAttack;

    public int damage;
    public int START_DAMAGE;
    public int BAT_DAMAGE;
    public int WOLF_DAMAGE;



    void Start()
    {
        damage = START_DAMAGE;
    }


    public bool PlayerCanAttackEnemy()
    {
        if (TimeRemaining <= 0)
        {
            CanAttack = true;
            if (DidAttack)
            {
                DidAttack = false;
                TimeRemaining = TimeBetween;
            }
        }
        else
        {
            CanAttack = false;
            TimeRemaining -= Time.deltaTime;
        }
        return CanAttack;
    }

    // Depreciated
    public void PlayerAttackEnemy()
    {
        if (TimeRemaining <= 0)
        {
            TimeRemaining = TimeBetween;
        }
        else
        {
            TimeRemaining -= Time.deltaTime;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().inPlayerRange = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().inPlayerRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().inPlayerRange = false;
        }
    }
}
