using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFollowPlayerScript : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    void LateUpdate()
    {
        Vector3 holdPos = transform.position;

        holdPos.x = playerTransform.position.x;
        holdPos.y = playerTransform.position.y;

        transform.position = holdPos;
    }
}