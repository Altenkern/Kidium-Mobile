using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartPanelControll : MonoBehaviour
{
    public Button LL, L, Mid, R, RR;
    public Image circleImage, cornerImg;
    public static PartPanelControll Instance;
    //public Vector3[] poses = new Vector3[5]; 
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
        objs.Add(cornerImg);
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
    public static GameObject static_getPanel()
    {
        return Instance.getPanel();
    }

    private void Update()
    {
        circleImage.color = circleImage.sprite == null ? new Color(255, 255, 255, 0) : new Color(255, 255, 255, 255);


        //LL.GetComponent<RectTransform>().anchoredPosition = poses[0];
        //L.GetComponent<RectTransform>().anchoredPosition = poses[1];
        //Mid.GetComponent<RectTransform>().anchoredPosition = poses[2];
        //R.GetComponent<RectTransform>().anchoredPosition = poses[3];
        //RR.GetComponent<RectTransform>().anchoredPosition = poses[4];
    }
}
