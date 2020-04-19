using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerScript : MonoBehaviour
{
    private bool InputTrue;
    public int InputMode;
    private int NumberOfModes = 3;

    public MovePlayerScript wasd;
    public _GM_Script gm;
    public PlayerControllerScript playerHandle;
    public GameMenuScript menu;

    /*
     * Input Mode:
     * 
     * 0 - player control
     * 1 - menu control
     * 2 - dialogue control
     * 
     * Input Controls:
     * 
     * KBM:
     * WASD/Arrow Keys  = move character / menu selection
     * E                = interact
     * Q                = cancel
     * I                = use item
     * M                = menu
     * SPACE            = attack
     * SHIFT            = transform
     * F                = flashlight
     * TAB /SHIFT       = toggle item
     * 
     * CONTROLLER:
     * D-pad/Stick      = move character / menu selection
     * A/Cross          = interact
     * B/Circle         = cancel
     * X/Square         = use item
     * Y/Triangle       = menu
     * Right-Trigger    = attack
     * Left-Trigger     = transform
     * Right-Bumper     = flashlight
     * Left-Bumper      = toggle item
     * 
     * ESC              = quit game w/out saving
     */


    void Start()
    {
        playerHandle.PlayerFlashCheck();
        menu = GameObject.Find("GameMenu").GetComponent<GameMenuScript>();

        // Determine what state the game is in at start to know whether to get / ignore input    

        // Checks scene / Game State to determine Input True/Mode
        InputMode = 0;
        // Should be defaulted to false, until fade in over
        InputTrue = true;
    }


    public void SetInputFalse()
    {
        InputTrue = false;
    }
    public void SetInputTrue()
    {
        InputTrue = true;
    }

    public void SetInputMode(int modeNum)
    {
        // Change the input mode
        if (modeNum >= 0 && modeNum <= NumberOfModes-1)
            InputMode = modeNum;
    }

    public bool GetInputTrue()
    {
        // Check conditions to see if we are getting player input right now
        // Not getting input during cutscenes/transitions/teleport fades

        return InputTrue;
    }

    void PlayerControlInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        PlayerMovement();
        PlayerControl();
    }

    void MenuControlInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        MenuControl();
    }

    void DialogueControlInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        // Confirm button
    }

    void MenuControl()
    {
        // Priority: 
        // Menu, Cancel, Interact, Attack, Item, Transform, Flashlight

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("CANCEL BUTTON.");
            menu.CancelMenuHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("CANCEL BUTTON.");
            menu.CancelMenuHandler();
        }
        /*
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("INTERACT BUTTON.");
            menu.SelectMenuHandler();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("INTERACT BUTTON.");
            menu.SelectMenuHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("INTERACT BUTTON.");
            menu.SelectMenuHandler();
        }
        */
    }


    void PlayerMovement()
    {
        /*
        Debug.Log("DEFAULT X: " + Input.GetAxisRaw("Horizontal"));
        Debug.Log("DEFAULT Y: " + Input.GetAxisRaw("Vertical"));
        Debug.Log("JOYSTICK Y: " + Input.GetAxisRaw("DPAD_Vertical"));
        Debug.Log("JOYSTICK X: " + Input.GetAxisRaw("DPAD_Horizontal"));
        Debug.Log("PLAYSTATION Y: " + Input.GetAxisRaw("DS4_Vertical"));
        Debug.Log("PLAYSTATION X: " + Input.GetAxisRaw("DS4_Horizontal"));
        Debug.Log("Horizontal: " + wasd.movement.x);
        Debug.Log("Vertical: " + wasd.movement.y);
        */

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if(Input.GetAxisRaw("DPAD_Horizontal") == 0)
            {
                if(Input.GetAxisRaw("DS4_Horizontal") == 0)
                {
                    wasd.movement.x = 0;
                }
                else
                {
                    wasd.movement.x = Input.GetAxisRaw("DS4_Horizontal");
                }
            }
            else
            {
                wasd.movement.x = Input.GetAxisRaw("DPAD_Horizontal");
            }
        }
        else
        {
            wasd.movement.x = Input.GetAxisRaw("Horizontal");
        }


        if (Input.GetAxisRaw("Vertical") == 0)
        {
            if (Input.GetAxisRaw("DPAD_Vertical") == 0)
            {
                if (Input.GetAxisRaw("DS4_Vertical") == 0)
                {
                    wasd.movement.y = 0;
                }
                else
                {
                    wasd.movement.y = Input.GetAxisRaw("DS4_Vertical");
                }
            }
            else
            {
                wasd.movement.y = Input.GetAxisRaw("DPAD_Vertical");
            }
        }
        else
        {
            wasd.movement.y = Input.GetAxisRaw("Vertical");
        }

        if (gm.player.IsWolf)
            wasd.speed = wasd.WOLF_SPEED;
        else
            wasd.speed = wasd.PLAYER_SPEED;

        if (wasd.movement.x != 0 || wasd.movement.y != 0)
            wasd.playerMoveTrue = true;
        else
            wasd.playerMoveTrue = false;
    }


    void PlayerAttack()
    {
        if (playerHandle.attack.PlayerCanAttackEnemy())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ATTACK BUTTON.");
                playerHandle.PlayerAttack();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                Debug.Log("ATTACK BUTTON.");
                playerHandle.PlayerAttack();
            }
        }
    }

    void PlayerControl()
    {
        // Priority: 
        // Menu, Cancel, Interact, Attack, Item, Transform, Flashlight


        if (playerHandle.attack.PlayerCanAttackEnemy())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ATTACK BUTTON.");
                playerHandle.PlayerAttack();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                Debug.Log("ATTACK BUTTON.");
                playerHandle.PlayerAttack();
            }
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("MENU BUTTON.");
            menu.OpenMenuHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("MENU BUTTON.");
            menu.OpenMenuHandler();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("CANCEL BUTTON.");
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("CANCEL BUTTON.");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("INTERACT BUTTON.");
            // For Items this is handled elsewhere...
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("INTERACT BUTTON.");
            // For Items this is handled elsewhere...
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("ITEM BUTTON.");
            playerHandle.PlayerItemUseHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("ITEM BUTTON.");
            playerHandle.PlayerItemUseHandler();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("TRANSFORM/RAYGUN BUTTON.");
            playerHandle.PlayerLeftTriggerHandler();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("TRANSFORM/RAYGUN BUTTON.");
            playerHandle.PlayerLeftTriggerHandler();
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("TRANSFORM/RAYGUN BUTTON.");
            playerHandle.PlayerLeftTriggerHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Debug.Log("TRANSFORM/RAYGUN BUTTON.");
            playerHandle.PlayerLeftTriggerHandler();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("FLASHLIGHT BUTTON.");
            playerHandle.PlayerFlashlightHandler();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("FLASHLIGHT BUTTON.");
            playerHandle.PlayerFlashlightHandler();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!playerHandle.player.IsWolf)
            {
                Debug.Log("CYCLE ITEM BUTTON.");
                playerHandle.PlayerCycleItemHandler();
            }
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (!playerHandle.player.IsWolf)
            {
                Debug.Log("CYCLE ITEM BUTTON.");
                playerHandle.PlayerCycleItemHandler();
            }
        }

        /*
        if (Input.GetKeyUp(KeyCode.T))
        {
            Debug.Log("RELEASED TRANSFORM/RAYGUN BUTTON.");
            playerHandle.player.RayActive = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("RELEASED TRANSFORM/RAYGUN BUTTON.");
            playerHandle.player.RayActive = false;
        }
        else if (Input.GetKeyUp(KeyCode.RightShift))
        {
            Debug.Log("RELEASED TRANSFORM/RAYGUN BUTTON.");
            playerHandle.player.RayActive = false;
        }
        else if (Input.GetKeyUp(KeyCode.JoystickButton6))
        {
            Debug.Log("RELEASED TRANSFORM/RAYGUN BUTTON.");
            playerHandle.player.RayActive = false;
        }
        */
    }

    void Update()
    {
        if (gm.playState == _GM_Script.PlayState.MENU)
            InputMode = 1;
        else if (gm.playState == _GM_Script.PlayState.PLAYER)
            InputMode = 0;
        else if (gm.playState == _GM_Script.PlayState.DIALOGUE)
            InputMode = 2;

        if (gm.playState != _GM_Script.PlayState.PLAYER)
        {
            playerHandle.wasd.StopPlayerMove();
        }


        // check if getting input true before anything
        if (InputTrue)
        {
            if (InputMode == 0) // player control mode
            {
                PlayerControlInput();
            }
            else if (InputMode == 1) // menu control mode
            {
                MenuControlInput();
            }
            else if (InputMode == 2) // dialogue control mode / credits / etc
            {
                // Handled elsewhere
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
        
    }


    void TestPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
            Debug.Log("Joystick Button 0 was pressed.");
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Space was pressed.");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
        }
    }



}
