using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Quest4 : Quest
{
    [System.Serializable]
    public class Quest4Item
    {
        public Sprite[] sprite;
        public AudioClip questGive;
        public AudioClip FoundHere;
        public AudioClip itIs;
        public AudioClip questOut;
    }


    [SerializeField] GameObject UIPanel;
    [SerializeField] Button btn0, btn1, btn2, btn3, btn4, btn5;
    
    [SerializeField] GameObject placeSprite;
    [SerializeField] QuestVideoControll videoControll;

    int countOfRight = 0;
    int questStage = 0;
    [SerializeField] int searchedID = -1;


    [SerializeField] List<int> buttonToItem = new List<int>() { 0, 0, 1, 1, 2, 2};

    StarEffectAnimControll[] starsBTNs = new StarEffectAnimControll[6];


    [SerializeField] Quest4Item squares;
    [SerializeField] Quest4Item triangles;
    [SerializeField] Quest4Item circles;

    public AudioClip eto;
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
        //ResetBtnToItems();


        ButtonSetSprites();

        //audio quest   
        GiveQuest();
    }

    Quest4Item getById(int id)
    {
        switch(id)
        {
            case 0:
                return squares;
            case 1:
                return triangles;
            case 2:
                return circles;
            default:
                return null;
        }
    }
    void ButtonSetSprites()
    {
        btn0.GetComponent<Image>().sprite = buttonToItem[0] == 0 ? squares.sprite[0+questStage*2] : buttonToItem[0] == 1 ? triangles.sprite[0+questStage*2] : circles.sprite[0+questStage*2];
        btn1.GetComponent<Image>().sprite = buttonToItem[1] == 0 ? squares.sprite[1+questStage*2] : buttonToItem[1] == 1 ? triangles.sprite[1+questStage*2] : circles.sprite[1+questStage*2];
        btn2.GetComponent<Image>().sprite = buttonToItem[2] == 0 ? squares.sprite[0+questStage*2] : buttonToItem[2] == 1 ? triangles.sprite[0+questStage*2] : circles.sprite[0+questStage*2];
        btn3.GetComponent<Image>().sprite = buttonToItem[3] == 0 ? squares.sprite[1+questStage*2] : buttonToItem[3] == 1 ? triangles.sprite[1+questStage*2] : circles.sprite[1+questStage*2];
        btn4.GetComponent<Image>().sprite = buttonToItem[4] == 0 ? squares.sprite[0+questStage*2] : buttonToItem[4] == 1 ? triangles.sprite[0+questStage*2] : circles.sprite[0+questStage*2];
        btn5.GetComponent<Image>().sprite = buttonToItem[5] == 0 ? squares.sprite[1+questStage*2] : buttonToItem[5] == 1 ? triangles.sprite[1+questStage*2] : circles.sprite[1+questStage*2];

    }

    void ResetBtnToItems()
    {
        for (int i = 0; i < 6; i++)
        {
            int temp = buttonToItem[i];
            int randomIndex = Random.Range(i, buttonToItem.Count);
            buttonToItem[i] = buttonToItem[randomIndex];
            buttonToItem[randomIndex] = temp;
        }
    }


    public void OnButtonClick(int i)
    {
        if(isClickable && !isEnded)
            if (searchedID == buttonToItem[i])
                OnRightClick(i);
            else
                OnWrongClick(i);

    }


    public void OnRightClick(int i)
    {
        starsBTNs[i].BurstStars();
        //audio good

        AudioController.ClearQueue_Static();
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { getById(questStage).questOut});
        countOfRight++;
        if (countOfRight >= 2)
            NextStage();
        else
        {
            //play another
        }
    }

    void NextStage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2.5f);
        sequence.onComplete += loadNextQuest;
    }
    void loadNextQuest()
    {
        countOfRight = 0;
        questStage++;
        GiveQuest();
        //ResetBtnToItems();
        if (searchedID < 3)
            ButtonSetSprites();

    }

    void OnWrongClick(int i)
    {
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { eto, getById(buttonToItem[i]).itIs });

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
        searchedID++;
        if (searchedID < 3)
        {
            switch (searchedID)
            {
                case 0:
                    Debug.Log("Square");
                    break;
                case 1:
                    Debug.Log("triangle");
                    break;
                case 2:
                    Debug.Log("circle");
                    break;
                default:
                    break;
            }

            AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { getById(questStage).questGive });
        }
        else
            OnComplete();

        //audio
    }


    public override void OnComplete()
    {
        isEnded = true;
        Debug.Log("Quest completed");
        //audio
        //next.StartQuest();

        videoControll.Play2ndVideo();
        StartCoroutine(LateComplete());
    }
    IEnumerator LateComplete()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Fade(0f));

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
