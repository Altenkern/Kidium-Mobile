using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quest1 : Quest
{
    [System.Serializable]
    public class Quest1Item
    {
        public Sprite sprite;
        public AudioClip questGive;
        public AudioClip itIs;
        public AudioClip questOut;
    }


    [SerializeField] GameObject UIPanel;
    [SerializeField] Button btn1, btn2, btn0;
    [SerializeField] Quest1Item[] Items = new Quest1Item[3];

    public AudioClip QuestStart;
    public AudioClip eto, naidi;
    public AudioClip Molodets;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeSpeed = 0.3f;

    [SerializeField] StarEffectAnimControll[] starsBTNs = new StarEffectAnimControll[3];

    int countOfRight = 0;
    [SerializeField] int searchedID = -1;


    [SerializeField] List<int> buttonToItem = new List<int>() { 0, 1, 2 };

    private void Awake()
    {
        starsBTNs[0] = btn0.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[1] = btn1.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
        starsBTNs[2] = btn2.transform.GetChild(0).GetComponent<StarEffectAnimControll>();
    }
    
    public override void StartQuest()
    {


        StartCoroutine(Fade(1f, true));

        //btns = new List<Button>();
        //if (Random.Range(0, 2) == 1)
        //    btns.Add(btn2);
        //else
        //    btns.Insert(0, btn2);
        //if (Random.Range(0, 2) == 1)
        //    btns.Add(btn1);
        //else
        //    btns.Insert(0, btn1);
        //if (Random.Range(0, 2) == 1)
        //    btns.Add(btn0);
        //else
        //    btns.Insert(0, btn0);

        

        for (int i = 0; i < 3; i++)
        {
            int temp = buttonToItem[i];
            int randomIndex = Random.Range(i, buttonToItem.Count);
            buttonToItem[i] = buttonToItem[randomIndex];
            buttonToItem[randomIndex] = temp;
        }

        btn0.GetComponent<Image>().sprite = Items[buttonToItem[0]].sprite;
        btn1.GetComponent<Image>().sprite = Items[buttonToItem[1]].sprite;
        btn2.GetComponent<Image>().sprite = Items[buttonToItem[2]].sprite;
        //audio quest
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { QuestStart });
        GiveQuest();
    }



    public void OnButtonClick(int i)
    {
        if (isClickable&&!isEnded)
            if (searchedID == i)
                OnRightClick(i);
            else
                OnWrongClick(i);

    }


    public void OnRightClick(int i)
    {
        starsBTNs[i].BurstStars();

        //audio good
        AudioController.ClearQueue_Static();
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { Items[buttonToItem[searchedID]].questOut });

        countOfRight++;
        if (countOfRight >= 2)
            OnComplete();
        else
            GiveQuest();
    }
    void OnWrongClick(int i)
    {

        List<AudioClip> wrongList = new List<AudioClip>();

        wrongList.Add(eto);
        wrongList.Add(Items[buttonToItem[i]].itIs);

        AudioController.AddSoundAndPlay_Static(wrongList);

        if (i == 0)
            HighlightObject(btn0.gameObject, 2f);
        else if (i == 1)
            HighlightObject(btn1.gameObject, 2f);
        else if (i == 2)
            HighlightObject(btn2.gameObject, 2f);

    }

    void GiveQuest()
    {
        if (searchedID == -1)
            searchedID = Random.Range(1, 3);
        else
            searchedID = 0;

        Debug.Log(Items[buttonToItem[searchedID]].sprite.name);

        List<AudioClip> wrongList = new List<AudioClip>();

        wrongList.Add(naidi);
        wrongList.Add(Items[buttonToItem[searchedID]].itIs);

        AudioController.AddSoundAndPlay_Static(wrongList);
        //audio

    }


    public override void OnComplete()
    {
        isEnded = true;
        Debug.Log("Quest completed");
        //AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { Items[buttonToItem[searchedID]].questOut });
        //audio
        StartCoroutine(LateComplete());
    }
    IEnumerator LateComplete()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(Fade(0f));
        next.StartQuest();

    }

    IEnumerator Fade(float targetAlpha, bool fast= false)
    {
        if (targetAlpha == 0)
            UIPanel.SetActive(false);
        else
            UIPanel.SetActive(true);

        if (fast)
            canvasGroup.alpha = targetAlpha;
        else
        while (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
