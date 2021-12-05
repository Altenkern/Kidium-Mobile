using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerController : MonoBehaviour
{
    static CornerController Instance;
    Animator animator;
    CanvasGroup canvasGroup;
    public delegate void AnimEnd();
    AnimEnd endAnim;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }


    public static void MakeCorner(AnimEnd anim)
    {
        Instance.endAnim = null;
        Instance.endAnim = anim;
        Instance.animator.SetTrigger("Create");
    }
    public static void HideCorner(AnimEnd anim)
    {
        Instance.endAnim = null;
        Instance.endAnim = anim;
        Instance.animator.SetTrigger("Hide");
    }
    public static void SetVisible(float f)
    {
        Instance.canvasGroup.alpha = f;
    }

    public void AnimEndTrigger()
    {
        if (endAnim != null)
            endAnim.Invoke();
    }
}
