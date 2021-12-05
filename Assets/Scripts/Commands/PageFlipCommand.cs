using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PageFlipCommand", menuName = "Scenery/Page Flip Command", order = 37)]
public class PageFlipCommand : Command
{
    public bool isEnter;
    public float delay = 0f;
    
    public override void Execute()
    {
        PageFlipEnd.PlayAnimStatic(Done);
        //UITransitor.static_MakeTransition(isEnter);
        //StartCoroutine(TimeUntillEnd());
    }

    public override void ResetCommand()
    {
    }

    public override void Stop()
    {
        UITransitor.static_StopTrans();
        //stopCoroutines(TimeUntillEnd());
    }

    IEnumerator TimeUntillEnd()
    {
        yield return new WaitForSeconds(delay);
        UITransitor.static_MakeTransition(isEnter);
        yield return new WaitForSeconds(1);
        Debug.Log("1 second is waited");
        Done();
    }
}
