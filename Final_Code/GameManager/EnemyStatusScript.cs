using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusScript : MonoBehaviour
{
    public enum AggroProgress { NONE, WATCHING, ONSIGHT, HUNTING }
    public AggroProgress aggro;
    public int aggroNum; // 0 none, 1 watching, 2 onsight, 3 hunting

    public int EnemyMaxHP = 50;

    public int NumEnemies;
    public int RemainingEnemies;

    public int[] EnemiesScene;
    public int[] EnemiesLocation;
    public int[] EnemiesHP;


    // enemy [scene num][enemy num][data0/1] = location, HP


    // Start is called before the first frame update
    void Start()
    {
        TestWave1();
    }

    public void TestWave1()
    {
        NumEnemies = 4;
        RemainingEnemies = NumEnemies;

        EnemiesScene = new int[NumEnemies];
        EnemiesLocation = new int[NumEnemies];
        EnemiesHP = new int[NumEnemies];

        for (int i = 0; i < NumEnemies; i++)
        {
            EnemiesScene[i] = i+4;
            EnemiesLocation[i] = 0;
            EnemiesHP[i] = EnemyMaxHP;
        }
    }

    public void UpdateAggroFromNum()
    {
        if (aggroNum == 0)
            aggro = AggroProgress.NONE;
        else if (aggroNum == 1)
            aggro = AggroProgress.WATCHING;
        else if (aggroNum == 2)
            aggro = AggroProgress.ONSIGHT;
        else if (aggroNum == 3)
            aggro = AggroProgress.HUNTING;
    }


    private void FixedUpdate()
    {
    }
}
