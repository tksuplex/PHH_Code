using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Code help from tutorial: https://www.youtube.com/watch?v=rhoQd6IAtDo

public class EnemyControllerScript : MonoBehaviour
{
    public GameStatusScript gameStatus;
    public AnimationControllerScript AnimControl;
    public EnemyAttackScript EnemyAttack;
    public float enemySpeed;
    public GameObject player;
    private Transform heatseeking;
    private float xDistance;
    private float yDistance;
    private bool FaceLeft;
    private bool FaceDown;
    private string lastFacing;

    // Start is called before the first frame update
    void Start()
    {
        xDistance = 0;
        yDistance = 0;
        FaceLeft = true;
        FaceDown = true;
        lastFacing = "down";

        heatseeking = player.transform;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void EnemyFacing()
    {
        xDistance = 0;
        yDistance = 0;
        FaceLeft = true;
        FaceDown = true;
        // heatseeking is player, transform is enemy which this script is attached to

        // get absolute x, y distance from enemy and player
        xDistance = Mathf.Abs(heatseeking.position.x - transform.position.x);
        yDistance = Mathf.Abs(heatseeking.position.y - transform.position.y);

        // determine which direction enemy should face right/left, up/down
        if (heatseeking.position.x > transform.position.x)
        {
            FaceLeft = false;
        }
        if (heatseeking.position.y > transform.position.y)
        {
            FaceDown = false;
        }

        // priortize animation based on greater x or y distance
        if (yDistance >= xDistance)
        {
            if (FaceDown)
            {
                AnimControl.EnemyFacingAnimate("enemydown");
                lastFacing = "down";
            }
            else
            {
                AnimControl.EnemyFacingAnimate("enemyup");
                lastFacing = "up";
            }
        }
        else
        {
            if (FaceLeft)
            {
                AnimControl.EnemyFacingAnimate("enemyleft");
                lastFacing = "left";
            }
            else
            {
                AnimControl.EnemyFacingAnimate("enemyright");
                lastFacing = "right";
            }
        }

    }

    void LateUpdate()
    {
        if (gameStatus.EnemyHP > 0 && (gameStatus.PlayerLocation == 0 || gameStatus.PlayerLocation == 6))
        {
            EnemyFacing();
            EnemyAttack.EnemyAttack(lastFacing);

            if (Vector2.Distance(transform.position, heatseeking.position) > 1.3)
            {
                transform.position = Vector2.MoveTowards(transform.position, heatseeking.position, enemySpeed * Time.deltaTime);
            }
        }
        else if (gameStatus.EnemyHP <= 0)
        {
            // enemy dead currently.
            AnimControl.EnemyFacingAnimate("enemyinvis");
        }
    }

}
