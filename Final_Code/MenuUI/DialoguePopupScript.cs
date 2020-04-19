using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;

public class DialoguePopupScript : MonoBehaviour
{
    public TextMeshProUGUI dialoguebox;


    public string[] dialogue;
    public int count;
    public int placeholder;
    public string line;

    public bool waiting;
    public bool nxtpls;
    public bool finished;

    _GM_Script gm;

    public GameObject hidepopup;

    private void Start()
    {
        gm = GameObject.Find("_GM").GetComponent<_GM_Script>();
        count = 0;
        placeholder = 0;
        finished = true;
        line = "";
    }


    public void PopupDialogue(int size, string[] given)
    {
        placeholder = 0;
        dialoguebox.text = "";
        gm.playState = _GM_Script.PlayState.DIALOGUE;
        hidepopup.SetActive(true);
        count = size;
        dialogue = given;
        finished = false;
        waiting = false;
        nxtpls = true;
    }

    public void OnlyLine(string line)
    {
        dialoguebox.text = line;
    }

    public IEnumerator DoWaitTime()
    {
        yield return new WaitForSeconds(0.4f);
        waiting = false;
        nxtpls = false;
    }

    public void DialogueInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            nxtpls = true;
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            nxtpls = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            nxtpls = true;
        }

    }

    public IEnumerator WaitBeforeControlBack()
    {
        yield return new WaitForSeconds(0.3f);

        gm.playState = _GM_Script.PlayState.PLAYER;
    }



    private void Update()
    {
        if (!nxtpls)
        {
            DialogueInputHandler();
/*
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                nxtpls = true;
            }
            else if (Input.anyKey)
            {
                nxtpls = true;
            }
*/
        }
        else if (!finished && nxtpls)
        {
            if (!waiting)
            {
                waiting = true;
                if (placeholder + 1 <= count)
                {
                    OnlyLine(dialogue[placeholder++]);
                    StartCoroutine(DoWaitTime());
                }
                else
                {
                    placeholder = 0;
                    waiting = false;
                    finished = true;
                    nxtpls = true;
                    StartCoroutine(WaitBeforeControlBack());
                }
            }
        }
        else
        {
            hidepopup.SetActive(false);

        }
    }
}
