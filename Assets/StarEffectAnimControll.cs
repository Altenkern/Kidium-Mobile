using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffectAnimControll : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void BurstStars()
    {
        anim.Play("StarsBurst");
    }
}
