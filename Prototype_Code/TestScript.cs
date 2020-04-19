using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;


    // Start is called before the first frame update
    void Start()
    {
        // EXAMPLE OF TELEPORTING PLAYER
        // TestTeleportPlayer(-3, 17, -1);

        // EXAMPLE OF CHANGING CAMERA Y POSITION ONLY BUT STILL FOLLOW PLAYER...
        // TestChangeCameraYAxis(5);
    }

    public void TestTeleportPlayer(int x, int y, int z)
    {
        // Debug.Log("player position= " + player.transform.position);
        player.transform.position = new Vector3(x, y, z);
        // Debug.Log("player position= " + player.transform.position);
    }

    public void TestChangeCameraYAxis(int yAxis)
    {
        // This does not work because camera follows player in LateUpdate
        // Have to add a conditional to that script, if you want to change the y axis, instead
        Vector3 holdPos = mainCam.transform.position;
        holdPos.y = yAxis;
        mainCam.transform.position = holdPos;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
