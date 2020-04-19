using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressScript : MonoBehaviour
{
    /*
     * PROGRESS OF GAME
     * 
     *  - START NEW GAME
     *  - INTRO CUTSCENE // NONE
     *  - lobby 1st time
     *  - dialogue & key // WATCHING
     *  - room call
     *  - lobby & guest key
     *  - guest room
     *  - mooks 1st fight // ON_SIGHT
     *  - girls dialogue
     *  - lobby mooks
     *  - dialogue & basement key
     *  - basement & clue
     *  - flashlight
     *  - lobby dialogue & clue -> ringing phone
     *  - answer phone dialogue (girls gone)
     *  - moonshine on desk
     *  - wolf scene on 3rd floor // WOLF UNLOCK
     *  - lobby empty
     *  - basement & master key
     *  - raygun parts dialogue 
     *  - if all parts // RAYGUN UNLOCK
     *  - if all crop scraps // LAST DOOR UNLOCK
     *  - find traveller, dialogue
     *  - clear path to roof
     *  - ENDING CUTSCENE
     */

    public int count;
    public string[] ViewProgress;
    public bool[] GameProgress;

    private void Start()
    {
        count = 25;
        GameProgress = new bool[count];
        for (int i = 0; i < count; i++)
        {
            GameProgress[i] = false;
        }

        SetupStringProgress();
    }

    public void SetupStringProgress()
    {
        ViewProgress = new string[count];
        int i = 0;
        ViewProgress[i++] = "NEW GAME";
        ViewProgress[i++] = "INTRO CUTSCENE";
        ViewProgress[i++] = "LOBBY 1ST TIME";
        ViewProgress[i++] = "GET OWN ROOM KEY";

    }

    public string ReturnStringCurrentProgress()
    {
        int highest = 0;
        for (int i = 0; i < count; i++)
        {
            if (GameProgress[i])
                highest++;
            else
                return ViewProgress[highest];
        }
        return ViewProgress[highest];
    }

    public int ReturnIntCurrentProgress()
    {
        int highest = 0;
        for (int i = 0; i < count; i++)
        {
            if (GameProgress[i])
                highest++;
            else
                return highest;
        }
        return highest;
    }

}
