using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum Aggro {  NONE, WATCHING, ONSIGHT, HUNTING }
    public Aggro aggro;
    public enum EnemyState { NONE, PATROL, ATTACK, PURSUIT }
    public EnemyState state;

    public int eMaxHP = 0;
    public bool flashOn;
    public int spawnNumber;
    public bool inPlayerRange;

    // For saving:
    public int eID = 0;
    public int eScene = 0;
    public int eLoc = 0;
    public int eHP = 0;

    public EnemyControllerScript controller;

    private void Start()
    {
        aggro = Aggro.NONE;
        state = EnemyState.NONE;
        flashOn = false;
    }

    public void DamageEnemy(int damage)
    {
        controller.DamageEnemy(damage);
    }

    public void DeleteEnemyObject()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        aggro = (Aggro)GameObject.Find("_GM").GetComponent<_GM_Script>().aggro;
    }
}
