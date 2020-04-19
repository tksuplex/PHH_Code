using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript gameInstace;

    // This keeps an instance of the Game Manager Object Open and ports
    // it across scenes without it being destroyed
    void Awake()
    {
        if (gameInstace == null)
            gameInstace = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
