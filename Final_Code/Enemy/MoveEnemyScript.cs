using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyScript : MonoBehaviour
{
    // This script just needs to move the enemy to where he needs to go
    // based on this enemy location, player location, or last known player location
    // based on all enemies aggro status
    // based on if this enemy has spotted player or not

    public enum EnemyFacing { UP, DOWN, LEFT, RIGHT }
    public EnemyFacing facing;
    public EnemyFacing desired;

    public EnemyControllerScript control;
    public EnemyScript enemy;
    public GameObject p;
    public Animator animate;
    public Animator animate2;

    public bool FACING_PLAYER;

    public Vector2 LastSceen;
    public bool seePlayer;
    public bool playerInFlash;
    public bool enemyMoving;
    public bool trackingPlayer;
    public float ENEMY_SPEED;

    PlayerScript player;
    float xLength;
    float yLength;

    float TimeBetween;
    private float TimeCountdown;


    private void Awake()
    {
        FACING_PLAYER = false;
        TimeBetween = 3.0f;
        TimeCountdown = 0;
        p = GameObject.Find("Player");
        player = p.GetComponent<PlayerScript>();
    }

    void Start()
    {
        xLength = 0f;
        yLength = 0f;
 
        enemyMoving = false;
        seePlayer = false;
        trackingPlayer = false;

        facing = EnemyFacing.DOWN;

        Physics2D.IgnoreCollision(p.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBlock")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void AnimateEnemyFace(string enemytrigger)
    {
        animate.SetTrigger(enemytrigger);
        animate2.SetTrigger(enemytrigger);
    }


    public bool CheckPlayerLoc()
    {
        if (enemy.eLoc == player.PlayerLoc)
            return true;
        else
            return false;
    }

    public void EnemyFacePlayer()
    {
        CanSeePlayer();
        if(seePlayer)
        {
            xLength = Mathf.Abs(p.transform.position.x - transform.position.x);
            yLength = Mathf.Abs(p.transform.position.y - transform.position.y);

            // possible values, 'enemyup', 'enemydown', 'enemyleft', 'enemyright', 'enemyinvis'
            if (yLength >= xLength)
            {
                if (p.transform.position.y > transform.position.y)
                {
                    facing = EnemyFacing.UP;
                    AnimateEnemyFace("enemyup");
                }
                else
                {
                    facing = EnemyFacing.DOWN;
                    AnimateEnemyFace("enemydown");
                }
            }
            else
            {
                if (p.transform.position.x > transform.position.x)
                {
                    facing = EnemyFacing.RIGHT;
                    AnimateEnemyFace("enemyright");
                }
                else
                {
                    facing = EnemyFacing.LEFT;
                    AnimateEnemyFace("enemyleft");
                }
            }
        }
    }

    public bool CheckFacingPlayer()
    {
        float x = Mathf.Abs(p.transform.position.x - transform.position.x);
        float y = Mathf.Abs(p.transform.position.y - transform.position.y);

        if (y >= x)
        {
            if (p.transform.position.y > transform.position.y)
            {
                desired = EnemyFacing.UP;
            }
            else
            {
                desired = EnemyFacing.DOWN;
            }
        }
        else
        {
            if (p.transform.position.x > transform.position.x)
            {
                desired = EnemyFacing.RIGHT;
            }
            else
            {
                desired = EnemyFacing.LEFT;
            }
        }

        if (desired == facing)
            return true;
        else
            return false;
    }

    public bool DarkAtEnemyLocation()
    {
        if (control.gm.location.DarkRoom[enemy.eLoc])
            return true;
        else
            return false;
    }

    public void SearchForPlayer()
    {
        if (CheckPlayerLoc() && TimeCountdown <= 0)
        {
            TimeCountdown = TimeBetween;
            int dir = Random.Range(0, 3);
            if (dir == 0)
            {
                facing = EnemyFacing.UP;
                AnimateEnemyFace("enemyup");
            }
            else if (dir == 1)
            {
                facing = EnemyFacing.DOWN;
                AnimateEnemyFace("enemydown");
            }
            else if (dir == 2)
            {
                facing = EnemyFacing.LEFT;
                AnimateEnemyFace("enemyleft");
            }
            else
            {
                facing = EnemyFacing.RIGHT;
                AnimateEnemyFace("enemyright");
            }
        }
        else
        {
            TimeCountdown -= Time.deltaTime;
        }
    }

    public void CanSeePlayer()
    {

        if (CheckPlayerLoc())
        {
            if (DarkAtEnemyLocation())
            {
                Debug.Log("dark at locaion");
                if (enemy.aggro == EnemyScript.Aggro.HUNTING && enemy.flashOn)
                {

                    if (playerInFlash)
                    {
                        seePlayer = true;
                        GetLastSeen();
                    }
                    else if (player.FlashOn && CheckFacingPlayer())
                    {
                        seePlayer = true;
                        GetLastSeen();
                    }
                    else
                    {
                        seePlayer = false;
                    }
                }
                else if (enemy.aggro == EnemyScript.Aggro.HUNTING)
                {
                    if (player.FlashOn && CheckFacingPlayer())
                    {
                        seePlayer = true;
                        GetLastSeen();
                    }
                    else
                    {
                        seePlayer = false;
                    }
                }
                else if (enemy.aggro == EnemyScript.Aggro.ONSIGHT)
                {
                    if (player.FlashOn && CheckFacingPlayer())
                    {
                        seePlayer = true;
                        GetLastSeen();
                    }
                    else
                    {
                        seePlayer = false;
                    }
                }
                else
                {
                    seePlayer = false;
                }
            }
            else
            {
                if (enemy.aggro == EnemyScript.Aggro.WATCHING)
                {
                    if (trackingPlayer)
                    {
                        seePlayer = true;
                        GetLastSeen();
                    }
                    else
                    {
                        float x = Mathf.Abs(p.transform.position.x - transform.position.x);
                        float y = Mathf.Abs(p.transform.position.y - transform.position.y);

                        // turns if player w/in a certain radius
                        if (x <= (float)(Screen.width / 2.75) && y <= (float)(Screen.height / 2.75))
                        {
                            seePlayer = true;
                            trackingPlayer = true;
                            GetLastSeen();
                        }
                        else
                        {
                            seePlayer = false;
                        }
                    }
                }
                else if (enemy.aggro == EnemyScript.Aggro.ONSIGHT)
                {
                    seePlayer = true;
                    GetLastSeen();
                }
                else if (enemy.aggro == EnemyScript.Aggro.HUNTING)
                {
                    seePlayer = true;
                    GetLastSeen();
                }
            }

        }
        else
        {
            seePlayer = false;
        }
    }

    public void GetLastSeen()
    {
        LastSceen = new Vector2(p.transform.position.x, p.transform.position.y);
    }


    public void AdvanceToPlayer()
    {
        enemyMoving = true;

        if(Vector2.Distance(transform.position, p.transform.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, p.transform.position, ENEMY_SPEED * Time.deltaTime);
        }
    }

    public void FixedUpdate()
    {
        if (enemy.eLoc == player.PlayerLoc)
        {
            LastSceen = new Vector2(p.transform.position.x, p.transform.position.y);
        }
    }

}
