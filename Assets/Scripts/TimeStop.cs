using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeStop : MonoBehaviour
{
    public bool doStop = true;

    public Sprite PauseSpr, PlaySpr;

    public void StopTime()
    { 
        Time.timeScale = doStop ? 0 : 1;
        GetComponent<Image>().sprite = doStop ? PlaySpr : PauseSpr;
        doStop = !doStop;
    }
}
