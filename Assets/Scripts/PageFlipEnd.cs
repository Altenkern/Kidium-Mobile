using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFlipEnd : MonoBehaviour
{
    static PageFlipEnd Instance;

    private Animator animator;

    public delegate void Done();
    Done done;
    private void Awake()
    {
        //print(Screen.currentResolution);
        if (Instance == null)
            Instance = this;

        animator = GetComponent<Animator>();
    }

    void PlayAnim(Done don)
    {
        done = null;
        done = don;
        
        //if (Screen.currentResolution.height/Screen.currentResolution.width == 16/10)
            animator.Play("PageFlip"); //16x10
        //else
        //    animator.Play("PageFlip16x9");

    }

    public static void PlayAnimStatic(Done don)
    {
        Instance.PlayAnim(don);
    }

    public void PageFlipped()
    {
        if(done!=null)
            done.Invoke();
    }
}
