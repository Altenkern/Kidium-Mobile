using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruAndVegPanelControll : MonoBehaviour
{
    public Button LL, L, mid, R, RR;
    public static FruAndVegPanelControll Instance;

    List<Button> buttons;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public List<Button> GetButtons()
    {
        List<Button> buttons = new List<Button>();
        buttons.Add(LL);
        buttons.Add(L);
        buttons.Add(mid);
        buttons.Add(R);
        buttons.Add(RR);
        return buttons;
    }

    public static List<Button> GetButtons_Static()
    {
        return Instance.GetButtons();
    }

    public GameObject getPanel()
    {
        return gameObject;
    }

    public static GameObject static_getPanel()
    {
        return Instance.getPanel();
    }
}
