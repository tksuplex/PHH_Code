using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerScript : MonoBehaviour
{
    public _GM_Script gm;

    public Transform enemyPrefab;
    public int numEnemy;

    public Transform[] spawnpoints;
    public int[] pointNum;

    // Start is called before the first frame update
    void Start()
    {


        TestWave1(enemyPrefab);
    }

    public void TestWave1(Transform e)
    {
        int ArraySize = (gm.gs.enemy.NumEnemies);
        int enemyID = 0;
        int spawn = 0;

        for (int i = 0; i < ArraySize; i++)
        {
            if (spawnpoints.Length > 0)
            {
                Transform sp = spawnpoints[spawn];
                e.name = "enemy_" + enemyID;
                Instantiate(e, sp.position, sp.rotation);

                string tempname = "enemy_" + i + "(Clone)";
                EnemyScript temp = GameObject.Find(tempname).GetComponent<EnemyScript>();
                temp.eID = i;
                temp.eHP = gm.gs.enemy.EnemiesHP[i];
                temp.eLoc = gm.gs.enemy.EnemiesLocation[i];
                temp.eScene = gm.gs.enemy.EnemiesScene[i];

                enemyID++;
                if (spawn == 0)
                    spawn++;
                else
                    spawn--;
            }
        }
    }

    void SpawnItem(Transform e)
    {
        Transform sp = spawnpoints[0];
        Instantiate(e, sp.position, sp.rotation);
        sp = spawnpoints[1];
        Instantiate(e, sp.position, sp.rotation);

    }
}
