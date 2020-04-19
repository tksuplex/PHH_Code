using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public EnemyScript enemy;
    public MoveEnemyScript wasd;
    public EnemyAttackScript attack;
    public GameObject enemyflash;
    public Animator enemyimpact;
    public Animator enemyattack;

    public _GM_Script gm;
    public Animator camshake;
    EnemyStatusScript es;
    bool handlingDeath;
    PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        handlingDeath = false;
        es = GameObject.Find("EnemyStatus").GetComponent<EnemyStatusScript>();
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        player = wasd.p.GetComponent<PlayerScript>();
        this.StartCoroutine(StartEnemy());
        camshake = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    public IEnumerator StartEnemy()
    {
        yield return new WaitForSeconds(0.1f);

        enemy.aggro = (EnemyScript.Aggro)gm.aggro;
    }

    public void DamageEnemy(int damage)
    {
        enemy.eHP -= damage;
        camshake.SetTrigger("camtremor");
        enemyimpact.SetTrigger("impactself");
    }

    public void EnemyDamagePlayer(int damage)
    {
        if (player.IsWolf)
        {
            player.PlayerHP -= (damage * 2);
        }
        else
        {
            player.PlayerHP -= damage;
        }
        camshake.SetTrigger("camtremor");

        if (wasd.facing == MoveEnemyScript.EnemyFacing.DOWN)
        {
            enemyattack.SetTrigger("impactdown");
        }
        else if (wasd.facing == MoveEnemyScript.EnemyFacing.UP)
        {
            enemyattack.SetTrigger("impactup");
        }
        else if (wasd.facing == MoveEnemyScript.EnemyFacing.LEFT)
        {
            enemyattack.SetTrigger("impactleft");
        }
        else if (wasd.facing == MoveEnemyScript.EnemyFacing.RIGHT)
        {
            enemyattack.SetTrigger("impactright");
        }
    }


    public void HandleEnemyDeath()
    {
        handlingDeath = true;

        // update this enemy hp in EnemyStatus
        es.EnemiesHP[enemy.eID] = 0;
        es.RemainingEnemies--;

        if (player.PlayerCharges > 0 && player.IsWolf)
        {
            player.PlayerCharges -= 2;
        }
        else if (player.PlayerCharges < 10 && player.WolfUnlock)
        {
            player.PlayerCharges += 1;
        }

        enemy.DeleteEnemyObject();
    }

    public void HandleEnemyWatching()
    {
        enemy.state = EnemyScript.EnemyState.NONE;

        if(wasd.CheckPlayerLoc())
        {
            wasd.EnemyFacePlayer();
        }        
    }

    public void HandleEnemyFlashlight()
    {
        if (wasd.DarkAtEnemyLocation() && enemy.aggro == EnemyScript.Aggro.HUNTING)
        {
            enemy.flashOn = true;
            enemyflash.SetActive(true);
        }
        else
        {
            wasd.playerInFlash = false;
            enemy.flashOn = false;
            enemyflash.SetActive(false);
        }    
    }


    public void HandleEnemyOnsight()
    {
        if(wasd.CheckPlayerLoc())
        {
            wasd.EnemyFacePlayer();
            if(wasd.seePlayer)
            {
                if (attack.playerInRange)
                {
                    wasd.enemyMoving = false;

                    if (attack.EnemyCanAttack())
                    {
                        EnemyDamagePlayer(attack.ENEMY_DAMAGE);
                    }
                }
                else
                {
                    wasd.AdvanceToPlayer();
                }
            }
            else
            {
                wasd.SearchForPlayer();
            }
        }
    }

    public void HandleEnemyHunting()
    {
        HandleEnemyFlashlight();
        HandleEnemyOnsight();
    }

    // Update is called once per frame
    void Update()
    {
        HandleEnemyFlashlight();
        if (enemy.eHP <= 0 && !handlingDeath)
        {
            HandleEnemyDeath();
        }

        if (gm.state.gameState == GameStateScript.GameState.PLAYING_GAME && gm.state.playState == GameStateScript.PlayState.PLAYER)
        {
            if (enemy.aggro == EnemyScript.Aggro.NONE)
            {
                enemy.state = EnemyScript.EnemyState.NONE;
            }
            else if (enemy.aggro == EnemyScript.Aggro.WATCHING)
            {
                HandleEnemyWatching();
            }
            else if (enemy.aggro == EnemyScript.Aggro.ONSIGHT)
            {
                HandleEnemyOnsight();
            }
            else if (enemy.aggro == EnemyScript.Aggro.HUNTING)
            {
                HandleEnemyHunting();
            }
        }
    }
}
