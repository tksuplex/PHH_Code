using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashlightScript : MonoBehaviour
{
    public MoveEnemyScript wasd;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wasd.playerInFlash = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           wasd.playerInFlash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wasd.playerInFlash = false;
        }
    }
}
