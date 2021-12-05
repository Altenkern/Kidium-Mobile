using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "BackgroundCommand", menuName = "Scenery/Background Command", order = 4)]
public class BackgroundCommand : Command
{
    public Sprite sprite;
    public MonoBehaviour _mb; // The surrogate MonoBehaviour that we'll use to manage this coroutine.
 
    public void StartCoroutine()
    {
        _mb = GameObject.FindObjectOfType<MonoBehaviour>();
        if (_mb != null)
        {
            Debug.Log("Found a MonoBehaviour.");
            _mb.StartCoroutine(TimeUntillEnd());
        }
        else
            Debug.Log("No MonoBehaviour object was found in the scene (which should basically be impossible).");
    }
    public override void Execute()
    {
        //ScenarioController.SetBackgroundImage_Static(sprite);
        StartCoroutine();

        FruAndVegPanelControll.static_getPanel().GetComponent<Image>().sprite = sprite;
        ShadowPanelControll.static_getPanel().GetComponent<Image>().sprite = sprite;
        ColorFruVegPanelControll.static_getPanel().GetComponent<Image>().sprite = sprite;
        PartPanelControll.static_getPanel().GetComponent<Image>().sprite = sprite;
    }

    public override void ResetCommand()
    {
        
    }

    public override void Stop()
    {
        
    }
    
    IEnumerator TimeUntillEnd() 
    {
        //ScenarioController.DeactiveBackgroundImage_Static();
        yield return new WaitForSeconds(1);
        Debug.Log("1 second is waited");
        Done();
    }
}
