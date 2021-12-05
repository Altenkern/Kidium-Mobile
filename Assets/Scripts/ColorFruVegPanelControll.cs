using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFruVegPanelControll : MonoBehaviour
{
    public Button LL, L, Mid, R, RR;
    public Image circleImage, cornerImage;
    
    public static ColorFruVegPanelControll Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public List<UnityEngine.Object> GetObjects()
    {
        List<UnityEngine.Object> objs = new List<UnityEngine.Object>();
        objs.Add(LL);
        objs.Add(L);
        objs.Add(Mid);
        objs.Add(R);
        objs.Add(RR);
        objs.Add(circleImage);
        objs.Add(cornerImage);
        return objs;
    }
    public static List<UnityEngine.Object> GetObjects_Static()
    {
        return Instance.GetObjects();
    }
    public GameObject getPanel()
    {
        return gameObject;
    }
    public static void PlaySound()
    {
        Instance.GetComponent<AudioSource>().Play();
    }
    public static GameObject static_getPanel()
    {
        return Instance.getPanel();
    }
}
