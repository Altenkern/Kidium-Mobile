using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Quest3 : Quest
{
    [System.Serializable]
    public class Quest1Item
    {
        public Sprite sprite;
        public bool isBig;
        public AudioClip questGive;
        public AudioClip itIs;
        public AudioClip size;
        public AudioClip questOut;
    }


    [SerializeField] GameObject UIPanel;
    [SerializeField] Button btn0, btn1, btn2, btn3, btn4, btn5;
    [SerializeField] Quest1Item[] Items = new Quest1Item[6];
    [SerializeField] GameObject placeSprite;
    public AudioClip eto;

    StarEffectAnimControll[] starsBTNs = new StarEffectAnimControll[6];
    int countOfRight = 0;
    int searchedID = -1;

    List<int> buttonToItem = new List<int>() {0,1,2,3,4,5};

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeSpeed = 0.3f;

    private void Awake()
    {
        starsBTNs[0] = btn0.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[1] = btn1.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[2] = btn2.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[3] = btn3.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[4] = btn4.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[5] = btn5.transform.GetChild(0).GetComponent<StarEffectAnimControll>();

    }

    public override void StartQuest()
    {
        StartCoroutine(Fade(1f));
        for (int i = 0; i < 6; i++)
        {
            int temp = buttonToItem[i];
            int randomIndex = Random.Range(i, buttonToItem.Count);
            buttonToItem[i] = buttonToItem[randomIndex];
            buttonToItem[randomIndex] = temp;
        }

        btn0.GetComponent<Image>().sprite = Items[buttonToItem[0]].sprite;
        btn0.transform.localScale = Vector3.one * (Items[buttonToItem[0]].isBig ? 1.3f : 0.7f); 
        btn1.GetComponent<Image>().sprite = Items[buttonToItem[1]].sprite;
        btn1.transform.localScale = Vector3.one * (Items[buttonToItem[1]].isBig ? 1.3f : 0.7f);
        btn2.GetComponent<Image>().sprite = Items[buttonToItem[2]].sprite;
        btn2.transform.localScale = Vector3.one * (Items[buttonToItem[2]].isBig ? 1.3f : 0.7f);
        btn3.GetComponent<Image>().sprite = Items[buttonToItem[3]].sprite;
        btn3.transform.localScale = Vector3.one * (Items[buttonToItem[3]].isBig ? 1.3f : 0.7f);
        btn4.GetComponent<Image>().sprite = Items[buttonToItem[4]].sprite;
        btn4.transform.localScale = Vector3.one * (Items[buttonToItem[4]].isBig ? 1.3f : 0.7f);
        btn5.GetComponent<Image>().sprite = Items[buttonToItem[5]].sprite;
        btn5.transform.localScale = Vector3.one * (Items[buttonToItem[5]].isBig ? 1.3f : 0.7f);

        GiveQuest();
        //audio quest
    }



    public void OnButtonClick(int i)
    {
        if (isClickable&&!isEnded)
            if (i == searchedID)
                OnRightClick(i);
            else
                OnWrongClick(i);

    }


    public void OnRightClick(int i)
    {
        starsBTNs[i].BurstStars();
        AudioController.ClearQueue_Static();
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { Items[buttonToItem[searchedID]].questOut });
        //audio good
        countOfRight++;
        if (countOfRight >= 2)
            OnComplete();
        else
            GiveQuest();
    }
    void OnWrongClick(int i)
    {
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { eto, Items[buttonToItem[i]].size, Items[buttonToItem[i]].itIs });

        if (i == 0)
            HighlightObject(btn0.gameObject, 2f);
        else if (i == 1)
            HighlightObject(btn1.gameObject, 2f);
        else if (i == 2)
            HighlightObject(btn2.gameObject, 2f);
        else if (i == 3)
            HighlightObject(btn3.gameObject, 2f);
        else if (i == 4)
            HighlightObject(btn4.gameObject, 2f);
        else if (i == 5)
            HighlightObject(btn5.gameObject, 2f);
        //audio
    }

    void GiveQuest()
    {
        if (searchedID == -1)
            searchedID = Random.Range(1, 2);
        else
            searchedID = 0;

        Debug.Log(Items[buttonToItem[searchedID]].sprite.name);
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { Items[buttonToItem[searchedID]].questGive, Items[buttonToItem[searchedID]].size, Items[buttonToItem[searchedID]].itIs });

        //audio
    }


    public override void OnComplete()
    {
        isEnded = true;
        Debug.Log("Quest completed");
        //audio
        StartCoroutine(LateComplete());
    }
    IEnumerator LateComplete()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(Fade(0f));
        next.StartQuest();

    }
    IEnumerator Fade(float targetAlpha)
    {
        if (targetAlpha == 0f)
            UIPanel.SetActive(false);
        else
            UIPanel.SetActive(true);

        while (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
