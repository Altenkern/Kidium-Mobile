using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitor : MonoBehaviour
{
    public static UITransitor instance;

    public Animator animator;
    
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        
    }

    private void Start()
    {
        StopTrans();
    }

    public void MakeTransition(bool isEnter)
    {
        if (isEnter)
            animator.Play("enterTransition", 0, 0f);
        else
            animator.Play("exitTranslation", 0, 0f);
        
    }

    public void StopTrans()
    {
        animator.StopPlayback();
    }

    void ResetTrans()
    {

    }

    public static void static_StopTrans()
    {
        instance.StopTrans();
    }


    public static void static_MakeTransition(bool isEnter)
    {
        instance.MakeTransition(isEnter);
    }
    
}
