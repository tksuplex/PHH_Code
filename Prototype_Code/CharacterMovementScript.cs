using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I utilized this tutorial: https://www.youtube.com/watch?v=yfsqai3ivyA

public class CharacterMovementScript : MonoBehaviour
{
    public StartMenuButtonScript startmenu;
    public GameStatusScript gameStatus;
    public AnimationControllerScript AnimControl;
    public PlayerAttackScript PlayerAttack;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float moveSpeed;
    public float MOVE_SPEED_BASE = 3.0f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public int xMove;
    public int yMove;
    public string lastFacing;

    // Start is called before the first frame update
    void Start()
    {
        xMove = 0;
        yMove = 0;
        lastFacing = "down";
    }

    private void MovePlayer()
    {
        rb.velocity = movement * moveSpeed * MOVE_SPEED_BASE;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("TOUCHED ENEMY!");
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("TOUCHED ENEMY!");
        }
    }
    */

    private void GetInput()
    {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("QUITTING GAME...");
            startmenu.LoadGameScene(0);
        }

        // Get x, y input from player
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        // Send x, y to orient player sprite
        PlayerFacing(horizontalMove, verticalMove);

        // Use x, y to move Player 
        movement = new Vector2(horizontalMove, verticalMove);
        moveSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();

        // Get attack/interact input
        if (gameStatus.ItemBat)
        {
            if (Input.GetKeyDown("space"))
            {
                PlayerAttack.PlayerAttack(lastFacing);
                //            PlayerAttack.PlayerAttack(horizontalMove, verticalMove);
            }
        }
    }

    void PlayerFacing(float x, float y)
    {
        if (x != 0)
        {
            if (x < 0)
            {
                AnimControl.PlayerFacingAnimate("playerleft");
                lastFacing = "left";
            }
            else
            {
                AnimControl.PlayerFacingAnimate("playerright");
                lastFacing = "right";
            }
        }
        else if (y != 0)
        {
            if (y < 0)
            {
                AnimControl.PlayerFacingAnimate("playerdown");
                lastFacing = "down";
            }
            else
            {
                AnimControl.PlayerFacingAnimate("playerup");
                lastFacing = "up";
            }
        }
    }

    private void AnimatePlayer(float x, float y)
    {
        // Animate character,
        // if horizontal is not zero use a left or right sprite, otherwise use an up or down sprite
    }

    void Update()
    {
        GetInput();
        MovePlayer();
    }

}
