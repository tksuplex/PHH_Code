using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamMeUpScript : MonoBehaviour
{
    public AnimationControllerScript AnimControl;
    public GameStatusScript gameStatus;
    public GameObject player;
    public GameObject enemy;
    private Transform playerBeam;
    private Transform enemyBeam;

    // This script is for rocketing player to correct location on the map.
    // Also must set/reset as needed for location change
    // ie. hallway spawn reset on re-enter.

    // Don't forget to set player location:
    // 0 - hall, 1- room 1, 2 - room 2...

    // REFERCENCE FOR DOOR LOCATIONS:
    // ENTER ROOM 1 
    // X: -141.97, Y: -2.73, FACING UP
    // ENTER ROOM 2
    // X: -101.78, Y: -2.73, FACING UP
    // ENTER ROOM 3
    // X: -57.78, Y: -2.73, FACING UP

    // ENTER HALL FROM ROOM 1:  1->0
    // X: -19.52, 2.66, FACING DOWN
    // ENTER HALL FROM ROOM 2: 2->0
    // X: -0.06, Y: 2.66, FACING DOWN
    // ENTER HALL FROM ROOM 3:
    // X: 18.39, Y: 2.66, FACING DOWN

    // ENTER HALL FROM ROOM 4:
    // X: -19.3, Y: -2.73, FACING UP
    // ENTER HALL FROM ROOM 5: 
    // X: 0.14, Y: -2.73, FACING UP
    // ENTER HALL FROM ROOM 6:
    // X: 18.2, Y: -2.73, FACING UP

    // ENTER ROOM 4
    // X: 56.53, Y: 2.66, FACING DOWN -> y 2.84
    // ENTER ROOM 5
    // X: 100.6, Y: 2.66, FACING DOWN
    // ENTER ROOM 6
    // X: 145.94, Y: 2.66, FACING DOWN


    // ENEMY SPAWN LOCATION
    // X: 18.65, Y: 0
    // ENEMY ROOM 6 SPAWN LOCATION
    // X: 145.94, Y: -2.73


    // Start is called before the first frame update
    void Start()
    {
    }

    public void TeleportRoom(int WhereTo)
    {
        // pause game
        gameStatus.PauseGame = true;

        int FromWhere = gameStatus.PlayerLocation;

        float xVal = getX(FromWhere, WhereTo);
        float yVal = getY(FromWhere, WhereTo);

        // warp
        PlayerWarp(xVal, yVal);

        // animate player facing right direction
        SendPlayerFaceDir(FromWhere, WhereTo);

        ResetEnemy(WhereTo);

        // update player location
        gameStatus.PlayerLocation = WhereTo;

        // unpause game
        gameStatus.PauseGame = false;
    }

    void ResetEnemy(int WhereTo)
    {
        if (WhereTo == 0 && !(gameStatus.EnemyHP > 0))
        {
            gameStatus.EnemyHP = gameStatus.EnemyMaxHP;

            enemyBeam = enemy.transform;
            Vector3 tempPosition = transform.position;

            tempPosition.x = 18.65f;
            tempPosition.y = 0f;

            enemy.transform.position = tempPosition;
        }
        else if (WhereTo == 6)
        {
            gameStatus.EnemyHP = gameStatus.EnemyMaxHP*44;

            enemyBeam = enemy.transform;
            Vector3 tempPosition = transform.position;

            // ENEMY ROOM 6 SPAWN LOCATION
            // X: 145.94, Y: -2.73
            tempPosition.x = 145.94f;
            tempPosition.y = -2.73f;

            enemy.transform.position = tempPosition;
        }
    }

    void SendPlayerFaceDir(int FromWhere, int WhereTo)
    {
        if (WhereTo == 0)
        {
            if (FromWhere < 4)
                AnimControl.PlayerFacingAnimate("playerdown");
            else
                AnimControl.PlayerFacingAnimate("playerup");
        }
        else if (WhereTo < 4)
            AnimControl.PlayerFacingAnimate("playerup");
        else
            AnimControl.PlayerFacingAnimate("playerdown");
    }

    void PlayerWarp(float x, float y)
    {
        playerBeam = player.transform;
        Vector3 tempPosition = transform.position;

        tempPosition.x = x;
        tempPosition.y = y;

        player.transform.position = tempPosition;
    }

    float getX(int FromWhere, int WhereTo)
    {
        if (WhereTo == 0)
        {
            if (FromWhere == 1)
                return -19.52f;
            else if (FromWhere == 2)
                return -0.06f;
            else if (FromWhere == 3)
                return 18.39f;
            else if (FromWhere == 4)
                return -19.3f;
            else if (FromWhere == 5)
                return 0.14f;
            else
                return 18.2f;
        }
        else if (WhereTo == 1)
            return -141.97f;
        else if (WhereTo == 2)
            return -101.78f;
        else if (WhereTo == 3)
            return -57.78f;
        else if (WhereTo == 4)
            return 56.53f;
        else if (WhereTo == 5)
            return 100.6f;
        else
            return 145.94f;
    }

    float getY(int FromWhere, int WhereTo)
    {
        if (WhereTo == 0)
        {
            if (FromWhere < 4)
                return 2.66f;
            else
                return -2.73f;
        }
        else if (WhereTo < 4)
            return -2.73f;
        else
            return 2.66f;
    }

}

