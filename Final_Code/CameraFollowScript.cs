using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public SceneChangerScript scene;
    public GameStateScript state;
    public PlayerStatusScript ps;
    public GameObject player;
    private Transform transf;
    public bool StickyCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transf = player.transform;

        state = GameObject.Find("GameState").GetComponent<GameStateScript>();
        ps = GameObject.Find("PlayerStatus").GetComponent<PlayerStatusScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();
    }

    void CameraFollowYes()
    {
        if (state.gameState == GameStateScript.GameState.PLAYING_GAME)
        {
            if (scene.SceneIndex == 8)
            {
                StickyCamera = true;
            }
            else
            {
                StickyCamera = true;
            }
        }
        else
        {
            StickyCamera = false;
            Debug.Log("no sticky camera");
        }
    }

    public void CameraChangeVertical(float y)
    {
        Vector3 holdPos = transform.position;

        holdPos.y = transf.position.y;

        transform.position = holdPos;
    }

    public void CameraManualLocation(float x, float y)
    {
        Vector3 holdPos = transform.position;

        holdPos.x = transf.position.x;
        holdPos.y = transf.position.y;

        transform.position = holdPos;
    }


    private void Update()
    {
        Vector3 holdPos = transform.position;

        holdPos.x = transf.position.x;

        transform.position = holdPos;
    }

    // AFTER WEEKS AND WEEKS THIS LITERALLY STOPPED BEING CALLED AND I HAVE NO EARTHLY IDEA WHY.........
    /*
void Lateupdate()
{
    Debug.Log("in late update1");
    Vector3 holdPos = transform.position;

    holdPos.x = transf.position.x;

    transform.position = holdPos;
    CameraFollowYes();
    if (StickyCamera)
    {
        Debug.Log("DO STICKY CAMERA");
        Vector3 holdPos = transform.position;

        holdPos.x = transf.position.x;

        transform.position = holdPos;
    }
}
    */
}
