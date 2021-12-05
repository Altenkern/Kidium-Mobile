using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class Quest : MonoBehaviour
{
    [SerializeField] public bool isUIQuest;

    [SerializeField] public Quest next;

    internal bool isClickable = true;
    internal bool isEnded = false;
    public abstract void StartQuest();
    public abstract void OnComplete();


    internal void HighlightObject(GameObject go, float awaitTime)
    {
        go.GetComponent<RectTransform>().SetAsLastSibling();
        isClickable = false;
        float startScale = go.transform.localScale.x;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(go.transform.DOScale(1.25f * startScale, 1f));
        sequence.AppendInterval(awaitTime);
        sequence.Append(go.transform.DOScale(startScale, 1f));

        sequence.onComplete += () => { isClickable = true; };
    }
}
