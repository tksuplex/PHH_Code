using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationControllerScript : MonoBehaviour
{
    public Animator playerAnim;
    public Animator enemyAnim;
    public Animator flashAnim;
    public Animator cameraAnim;
    public Animator bubbleAnim;
    public Animator slashAnim;
    public Animator impactAnim;
    public Animator selfImpactAnim;


    // Start is called before the first frame update
    void Start()
    {
        // If any animations need to be set on scene load put them here...
        FlashlightOff();
    }

    // values: 'bubbledone', 'bubbleq', bubbledot, bubbledoor, bubblelock, bubbleskull, bubblehealth
    public void PlayerBubbleAnimate(string bubbletrigger)
    {
        bubbleAnim.SetTrigger(bubbletrigger);
    }

    // possible values, 'playerup', 'playerdown', 'playerleft', 'playerright'
    public void PlayerFacingAnimate(string playertrigger)
    {
        playerAnim.SetTrigger(playertrigger);       
    }

    // possible values, 'enemyup', 'enemydown', 'enemyleft', 'enemyright', 'enemyinvis'
    public void EnemyFacingAnimate(string enemytrigger)
    {
        enemyAnim.SetTrigger(enemytrigger);
    }

    // possible: pswipeup, pswipedown, pswiperight, pswipeleft, pswipenone
    public void PlayerSlashAnimate(string slashtrigger)
    {
        slashAnim.SetTrigger(slashtrigger);
    }

    // valuse: impactnone, impactup, impactdown, impactleft, impactright
    public void EnemyAttackImpact(string impacts)
    {
        impactAnim.SetTrigger(impacts);
    }

    public void EnemyGetsHurt()
    {
        selfImpactAnim.SetTrigger("impactself");
    }


    public void FlashlightOn()
    {
        flashAnim.SetTrigger("flashlighton");
    }

    public void FlashlightOff()
    {
        flashAnim.SetTrigger("flashlightoff");
    }

    public void ShakeCamera()
    {
        cameraAnim.SetTrigger("camtremor");
    }
}
