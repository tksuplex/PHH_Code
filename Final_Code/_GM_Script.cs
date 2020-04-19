using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _GM_Script : MonoBehaviour
{
    public enum GameState { MAIN_MENU, LOADING_GAME, SAVING_GAME, PLAYING_GAME, GAME_OVER, BEAT_GAME }
    public enum PlayState { NONE, WAITING, PLAYER, MENU, DIALOGUE }
    public enum AggroProgress { NONE, WATCHING, ONSIGHT, HUNTING }

    public GameState gameState;
    public PlayState playState;
    public AggroProgress aggro;
    public float UpdateTime = 0.1f;

    public PlayerScript player;
    public LocationControllerScript location;

    public GameStatusScript gs;
    public GameStateScript state;
    public EnemyStatusScript es;
    public LocationStatusScript loc;
    public SceneChangerScript scene;
    Scene scene2;

    public bool justStarted;


    void Start()
    {
        justStarted = false;
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        gs = GameObject.Find("GameStatus").GetComponent<GameStatusScript>();
        state = GameObject.Find("GameState").GetComponent<GameStateScript>();
        es = GameObject.Find("EnemyStatus").GetComponent<EnemyStatusScript>();
        loc = GameObject.Find("LocationStatus").GetComponent<LocationStatusScript>();
        scene = GameObject.Find("SceneChanger").GetComponent<SceneChangerScript>();

        this.StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        scene2 = SceneManager.GetActiveScene();
        LoadPlayer();
        LoadGamestate();
        LoadAggro();
        LoadLocation();

        if (gs.player.StartGame)
        {
            gs.player.StartGame = false;
            player.StartGame = false;
            Vector3 temp = transform.position;
            temp.x = 955;
            temp.y = -75;
            player.transform.position = temp;
        }

        else if (scene2.buildIndex == 4 && (gameState == GameState.SAVING_GAME || gameState == GameState.LOADING_GAME 
            || state.gameState == GameStateScript.GameState.LOADING_GAME
            || state.gameState == GameStateScript.GameState.SAVING_GAME))
        {
            Debug.Log("GM SCRIPT CAUSING SAVE GAME ISSUE");
            gameState = GameState.PLAYING_GAME;
            playState = PlayState.PLAYER;
            state.gameState = GameStateScript.GameState.PLAYING_GAME;
            state.playState = GameStateScript.PlayState.PLAYER;
            Vector3 temp = transform.position;
            temp.x = 955;
            temp.y = -75;
            player.transform.position = temp;
        }

        yield return new WaitForSeconds(UpdateTime);
        this.StartCoroutine(StartAutoUpdate());
    }

    public IEnumerator StartAutoUpdate()
    {
        CheckGameOver();
        UpdatePlayer();
        UpdateGamestate();
        UpdateAggro();
        UpdateLocation();

        yield return new WaitForSeconds(UpdateTime);
        this.StartCoroutine(StartAutoUpdate());
    }

    void CheckGameOver()
    {
        if (player.PlayerHP <= 0)
        {
            playState = PlayState.NONE;
            gameState = GameState.GAME_OVER;
            // Transfer to game over scene
            scene.FadeOutOnSceneExit(scene.fadeOutTime, 2);
        }

    }

    void LoadLocation()
    {
        location.PowerOn = loc.PowerOn;
        location.LocationTotal = loc.LocationTotal;
        location.DarkRoom = loc.DarkRoom;
        location.floor = (LocationControllerScript.CurrentFloor)loc.floor;

        scene2 = SceneManager.GetActiveScene();
        if (scene2.buildIndex == 7 || scene2.buildIndex == 8 || scene2.buildIndex == 9)
        {
            int rand = Random.Range(0, 9);

            if (player.WolfUnlock && rand <= 7)
            {
                location.PowerOn = false;
            }
            else if (player.FlashUnlock && rand <= 4)
            {
                location.PowerOn = false;
            }
            else if (rand <= 1)
            {
                player.BeenScared = false;
            }
            else
            {
                /*
                location.PowerOn = true;
                location.TurnPowerBackOn();
                */
            }
        }

        if (location.PowerOn)
        {
            location.TurnPowerBackOn();
        }
        else
        {
            location.TurnPowerOff();
        }
    }

    void UpdateLocation()
    {
        if (location.PowerOn)
        {
            location.TurnPowerBackOn();
        }
        else
        {
            location.TurnPowerOff();
        }
        loc.PowerOn = location.PowerOn;
        loc.floor = (LocationStatusScript.CurrentFloor)location.floor;
        loc.DarkRoom = location.DarkRoom;
    }

    void LoadGamestate()
    {
        gameState = (_GM_Script.GameState)state.gameState;
        playState = (_GM_Script.PlayState)state.playState;
    }

    void UpdateGamestate()
    {
        state.gameState = (GameStateScript.GameState)gameState;
        state.playState = (GameStateScript.PlayState)playState;
    }

    void LoadAggro()
    {
        aggro = (_GM_Script.AggroProgress)es.aggro;
    }

    void UpdateAggro()
    {
        es.aggro = (EnemyStatusScript.AggroProgress)aggro;
    }

    void LoadPlayer()
    {
        player.MoonShineNum = gs.player.MoonShineNum;
        player.HealthNum = gs.player.HealthNum;

        player.BeenScared = gs.player.BeenScared;
        player.TriggerFlash = gs.player.TriggerFlash;
        player.StartGame = gs.player.StartGame;
        player.item = (PlayerScript.PlayerItem)gs.player.item;
        player.PlayerHP = gs.player.PlayerHP;
        player.PlayerMaxHP = gs.player.PlayerMaxHP;
        player.PlayerCharges = gs.player.PlayerCharges;
        player.PlayerMaxCharges = gs.player.PlayerMaxCharges;
        player.PlayerLoc = gs.player.PlayerLoc;

        player.WolfUnlock = gs.player.WolfUnlock;
        player.FlashUnlock = gs.player.FlashUnlock;
        player.BatUnlock = gs.player.BatUnlock;
        player.RaygunUnlock = gs.player.RaygunUnlock;

        player.FlashOn = gs.player.FlashOn;
        player.IsWolf = gs.player.IsWolf;
    }

    void UpdatePlayer()
    {
        if (player.item == PlayerScript.PlayerItem.NONE)
        {
            player.UpdateItemEquip();
        }

        if (gs.item.ItemCollected[0])
        {
            player.FlashUnlock = true;
        }
        if (gs.item.ItemCollected[1])
        {
            player.BatUnlock = true;
        }
        if (gs.item.ItemCollected[2])
        {
            player.WolfUnlock = true;
        }
        if (gs.item.ItemCollected[3])
        {
            player.RaygunUnlock = true;
        }
        if (player.FlashUnlock && player.WolfUnlock && playState == PlayState.PLAYER)
        {
            aggro = AggroProgress.HUNTING;
        }
        else if (player.FlashUnlock && playState == PlayState.PLAYER)
        {
            aggro = AggroProgress.ONSIGHT;
        }
        else if (playState == PlayState.PLAYER)
        {
            aggro = AggroProgress.WATCHING;
        }
        else if (playState != PlayState.PLAYER)
        {
            aggro = AggroProgress.NONE;
        }

        /* causing problems:
        gs.item.MoonshineNum = player.MoonShineNum;
        gs.item.HealthNum = player.HealthNum;
        */
        gs.player.HealthNum = player.HealthNum;
        gs.player.MoonShineNum = player.MoonShineNum;
        gs.player.item = (PlayerStatusScript.PlayerItem)player.item;
        gs.player.PlayerHP = player.PlayerHP;
        gs.player.PlayerMaxHP = player.PlayerMaxHP;
        gs.player.PlayerCharges = player.PlayerCharges;
        gs.player.PlayerMaxCharges = player.PlayerMaxCharges;
        gs.player.PlayerLoc = player.PlayerLoc;
        gs.player.WolfUnlock = player.WolfUnlock;
        gs.player.FlashUnlock = player.FlashUnlock;
        gs.player.BatUnlock = player.BatUnlock;
        gs.player.RaygunUnlock = player.RaygunUnlock;
        gs.player.BeenScared = player.BeenScared;
        gs.player.TriggerFlash = player.TriggerFlash;
        gs.player.StartGame = player.StartGame;

        gs.player.FlashOn = player.FlashOn;
        gs.player.IsWolf = player.IsWolf;
    }

}
