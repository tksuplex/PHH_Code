using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScript : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    public bool conditionMet;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;        
    }

    void LateUpdate()
    {
        Vector3 holdPos = transform.position;

        holdPos.x = playerTransform.position.x;
        // TURNING OFF Y TO JUST FOLLOW X POSITION
        //        holdPos.y = playerTransform.position.y;


        // THIS WAS JUST A TEST: FIX LATER:
        /*
        if (conditionMet)
        {
            holdPos.y = 5;
        }
        */

        transform.position = holdPos;
    }
}
