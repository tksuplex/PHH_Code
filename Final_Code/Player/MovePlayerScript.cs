using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerScript : MonoBehaviour
{
    public enum PlayerFacing { UP, DOWN, LEFT, RIGHT };
    public PlayerFacing facing;
    // public Animator playerAnimator;
    // gamestatus maybe
    public Animator normal;
    public Animator wolf;
    public Animator eyes;
    public Animator green;

    public Rigidbody2D rb;
    public Vector2 movement;
    public float speed;
    public float PLAYER_SPEED;
    public float WOLF_SPEED;
    public bool playerMoveTrue;
    int multiplier;

    // Start is called before the first frame update
    void Start()
    {
        facing = PlayerFacing.DOWN;
        playerMoveTrue = false;
        PLAYER_SPEED = speed;
        WOLF_SPEED = speed * 1.35f;
        multiplier = 3;
    }

    void PlayerDirection(float x, float y)
    {
        if (x != 0)
        {
            if (x < 0)
            {
                facing = PlayerFacing.LEFT;
            }
            else
            {
                facing = PlayerFacing.RIGHT;
            }
        }
        else if (y != 0)
        {
            if (y < 0)
            {
                facing = PlayerFacing.DOWN;
            }
            else
            {
                facing = PlayerFacing.UP;
            }
        }
    }

    private void Update()
    {
        if (playerMoveTrue)
        {
            rb.MovePosition(rb.position + movement * speed * multiplier * Time.fixedDeltaTime);
            PlayerDirection(movement.x, movement.y);
        }

        if (facing == PlayerFacing.LEFT)
        {
            normal.SetTrigger("playerleft");
            wolf.SetTrigger("playerleft");
            eyes.SetTrigger("wolfleft");
            green.SetTrigger("playerleft");
        }
        else if (facing == PlayerFacing.RIGHT)
        {
            normal.SetTrigger("playerright");
            wolf.SetTrigger("playerright");
            eyes.SetTrigger("wolfright");
            green.SetTrigger("playerright");
        }
        else if (facing == PlayerFacing.DOWN)
        {
            normal.SetTrigger("playerdown");
            wolf.SetTrigger("playerdown");
            eyes.SetTrigger("wolfdown");
            green.SetTrigger("playerdown");
        }
        else if (facing == PlayerFacing.UP)
        {
            normal.SetTrigger("playerup");
            wolf.SetTrigger("playerup");
            eyes.SetTrigger("wolfup");
            green.SetTrigger("playerup");
        }

    }

    public void StopPlayerMove()
    {
        rb.velocity = Vector3.zero;
    }
}
