
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[CreateAssetMenu(fileName = "ColorFruAndVegCommand", menuName = "Scenery/Scenes/Color Fruit And Vegeatbles Command", order = 2)]
public class ColorFruAndVegCommand : Command
{
    public class coloredFrukt 
    {   
        public Frukt frukt;
        public Frukt color;
    }
    [SerializeField] private bool _doKeepObjectsEveryRound = false;

    [SerializeField] private Frukt RedColor, BlueColor, YellowColor, GreenColor, OrangeColor;

    public List<Frukt> vegetables;

    private List<coloredFrukt> currentList;
    
    private coloredFrukt searchedFrukt;
    //leftest, left, mid, right, rightest buttons
    public Button llBut, lBut, midBut, rBut, rrBut;
    public Image CircleIMG, CornerIMG;

    private Button[] _buttons = new Button[5];
    private ParticleSystem[] _buttonPSs = new ParticleSystem[5];

    private GameObject mainPanel;
    
    
    public List<AudioClip> AFKphrases, rightPhraes;

    public AudioClip Eto, Naidi, pokajiPlzGde, NajmiPalchikomNa, ASeychasPokajiGde;

    //private bool isFirstFrukt = true;
    
    private int currentItteration;
    private List<Frukt> red = new List<Frukt>(), green = new List<Frukt>(), yellow = new List<Frukt>(), blue = new List<Frukt>(), orange = new List<Frukt>();

    private List<Frukt> usedFrukt = new List<Frukt>(), usedColor = new List<Frukt>(), cycledFrukts = new List<Frukt>();

    #region Init Methods
    void InitGame()
    {
        //init game data
        red.Clear();
        green.Clear();
        yellow.Clear();
        blue.Clear();
        orange.Clear();
        usedFrukt.Clear();
        usedColor.Clear();
        cycledFrukts.Clear();

        currentItteration = 0;
        //isFirstFrukt = true;
        initButtons();
        initLists();
        RevealPanel();
        AFKTimer.SetAFKPhrases_Static(AFKphrases);
        AFKTimer.StartTimer_Static();
    }
    private void initLists()
    {
        for (int i = 0; i < vegetables.Count; i++)
        {
            for (int d = 0; d < vegetables[i]._fruktColor.Length; d++)
            {
                if (vegetables[i]._fruktColor[d] == RedColor && !red.Contains(vegetables[i]))
                    red.Add(vegetables[i]);
                else if (vegetables[i]._fruktColor[d] == GreenColor && !green.Contains(vegetables[i]))
                    green.Add(vegetables[i]);
                else if(vegetables[i]._fruktColor[d] == YellowColor && !yellow.Contains(vegetables[i]))
                    yellow.Add(vegetables[i]);
                else if(vegetables[i]._fruktColor[d] == OrangeColor && !orange.Contains(vegetables[i]))
                    orange.Add(vegetables[i]);
                else if(vegetables[i]._fruktColor[d] == BlueColor && !blue.Contains(vegetables[i]))
                    blue.Add(vegetables[i]);

            }
        }
    }    

    private void RevealPanel()
    {
        CanvasGroup canv = mainPanel.GetComponent<CanvasGroup>();
        canv.alpha = 1;
        canv.interactable = true;
        canv.blocksRaycasts = true;
    }
    
    private void HidePanel()
    {
        for (int i = 0; i < 5; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
        }

        CanvasGroup canv = mainPanel.GetComponent<CanvasGroup>();
        canv.alpha = 0;
        canv.interactable = false;
        canv.blocksRaycasts = false;
    }


    void initButtons()
    {
        //init main panel
        mainPanel = ColorFruVegPanelControll.static_getPanel();
        
        //init buttons
        List<UnityEngine.Object> butt = ColorFruVegPanelControll.GetObjects_Static();
        
        llBut     = (Button)butt[0];
        lBut      = (Button)butt[1];
        midBut    = (Button)butt[2];
        rBut      = (Button)butt[3];
        rrBut     = (Button)butt[4];
        CircleIMG = (Image)butt[5];
        CornerIMG = (Image)butt[6];
        
        
        _buttons[0] = llBut;
        _buttons[1] = lBut;
        _buttons[2] = midBut;
        _buttons[3] = rBut;
        _buttons[4] = rrBut;
        
        //events
        for (int i = 0; i < 5; i++)
        {
            _buttonPSs[i] = _buttons[i].GetComponentInChildren<ParticleSystem>();
            int x = i;
            _buttons[i].onClick.AddListener(() =>
            {

                ClickButton(x);
            });
        }
    }
    #endregion 
    void NewGame()
    {
        InitCurrentList();
        CornerController.SetVisible(1);
        RefreshButtonSprites();
        //bounceButtons(true).onComplete += () => AudioController.PlaySound_static(createQuestAudio(searchedFrukt));
        if (_doKeepObjectsEveryRound)
        {
            if (currentItteration == 0)
                bounceButtons(true).onComplete += () => { 
                    CornerController.MakeCorner(() => { AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt)); });
                };
            else
                AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt));
        }
        else
        {
            bounceButtons(true).onComplete += () => {
                CornerController.MakeCorner(() => { AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt)); });
            };
        }
    }


    void InitCurrentList()
    {
        if (!_doKeepObjectsEveryRound)
        {
            //init vegetables list

            makeCurrentList();


            searchedFrukt = getUnusedColorFruct();

            Debug.Log(searchedFrukt.color._name + " " + searchedFrukt.frukt._name + " is searched obj");
        }
        else
        {
            if (currentItteration == 0)
                makeCurrentList();
               
            
            searchedFrukt = getUnusedColorFruct();
        }
    }

    void makeCurrentList()
    {
        currentList = new List<coloredFrukt>();

        currentList.Add(new coloredFrukt() { frukt = yellow.OrderBy(x => Random.Range(0, 1000)).ToList()[0], color = YellowColor });
        currentList.Add(new coloredFrukt() { frukt = green.OrderBy(x => Random.Range(0, 1000)).ToList()[0], color = GreenColor });
        currentList.Add(new coloredFrukt() { frukt = orange.OrderBy(x => Random.Range(0, 1000)).ToList()[0], color = OrangeColor });
        currentList.Add(new coloredFrukt() { frukt = blue.OrderBy(x => Random.Range(0, 1000)).ToList()[0], color = BlueColor });
        currentList.Add(new coloredFrukt() { frukt = red.OrderBy(x => Random.Range(0, 1000)).ToList()[0], color = RedColor });

        currentList = currentList.OrderBy(x => Random.Range(0, 1000)).ToList();
    }
    coloredFrukt getUnusedColorFruct()
    {
        if (usedColor.Count < 5)
        {
            searchedFrukt = currentList[Random.Range(0, 5)];
            if (usedColor.Contains(searchedFrukt.color))
            {
                return getUnusedColorFruct();
            }
            else
            {
                usedColor.Add(searchedFrukt.color);
                return searchedFrukt;
            }
        }
        else
        {
            Debug.LogError("Can't make any more unused collours, repeat");
            usedColor.Clear();
            return getUnusedColorFruct();
        }
    }

    Sequence bounceButtons(bool inOut, bool doAdds = false)                                                                
    {                                                                                             
        Sequence bounceSequence = DOTween.Sequence();                                             
        for (int i = 0; i < 5; i++)                                                               
        {                                                                                         
            _buttons[i].transform.localScale = inOut ? Vector3.zero : Vector3.one;                
            if(inOut)                                                                             
            {                                                                                     
                bounceSequence.Insert(0,_buttons[i].transform.DOScale(1.2f, 0.6f))
                    .Insert(1,_buttons[i].transform.DOScale(0.95f, 0.4f))
                    .Insert(2,_buttons[i].transform.DOScale(1f, 0.2f));                
            }                                                                                     
            else                                                                                  
            {                                                                                     
                bounceSequence.Insert(0,_buttons[i].transform.DOScale(0.95f, 0.2f))
                    .Insert(1,_buttons[i].transform.DOScale(1.2f, 0.4f))
                    .Insert(2,_buttons[i].transform.DOScale(0f, 0.6f));
            }
        }
        if (inOut)
        {
            bounceSequence.Insert(0, CircleIMG.transform.DOScale(1f, 1.2f));
        }
        else
        {
            bounceSequence.Insert(0, CircleIMG.transform.DOScale(0f, 1.2f));
        }
        if (doAdds)
        {
            if (inOut)
            {
                bounceSequence.Insert(0, CornerIMG.DOFade(1f, 1.2f));
            }
            else
            {
                bounceSequence.Insert(0, CornerIMG.DOFade(0f, 1.2f));
            }
        }
        

        bounceSequence.timeScale = 5;
        return bounceSequence;
    }

    void RefreshButtonSprites()
    {
        if (currentList.Count != 5)
        {
            InitCurrentList();
        }
        else
        {
            
                //set sprites of buttons;
                for (int i = 0; i < 5; i++)
                {
                    for (int d = 0; d < currentList[i].frukt._sprites.Length; d++)
                    {
                        if (currentList[i].color == currentList[i].frukt._fruktColor[d])
                            _buttons[i].image.sprite = currentList[i].frukt._sprites[d];

                    }

                _buttons[i].image.SetNativeSize();
            }

            CircleIMG.sprite = searchedFrukt.color._colorSprite;
        }
    }

    List<AudioClip> createWrongAudio(coloredFrukt fruktWrong, coloredFrukt fruktRight€)
    {
        List<AudioClip> wrongList = new List<AudioClip>();

        wrongList.Add(Eto);
        wrongList.Add(fruktWrong.frukt._isFemineRod ? fruktWrong.color._archiRodPadezj : fruktWrong.color._archiImenPadezj);
        wrongList.Add(fruktWrong.frukt._archiImenPadezj);
        //wrongList.Add(Naidi);

        //wrongList.Add(fruktRight.frukt._isFemineRod ? fruktRight.color._archiRodPadezj : fruktRight.color._archiImenPadezj);
        //wrongList.Add(fruktRight.frukt._archiRodPadezj != null ? fruktRight.frukt._archiRodPadezj : fruktRight.frukt._archiImenPadezj);

        return wrongList;
    }
    List<AudioClip> CreatePraiseAudio()
    {
        List<AudioClip> questList = new List<AudioClip>();
        questList.Add(rightPhraes[Random.Range(0, rightPhraes.Count)]);
        return questList;
    }
    List<AudioClip> createQuestAudio(coloredFrukt fruktRight)
    {
        List<AudioClip> questList = new List<AudioClip>();

        //if (!isFirstFrukt)  
        //{
        //    questList.Add(rightPhraes[Random.Range(0, rightPhraes.Count)]);
        //}

        bool doImen = true;

        switch (Random.Range(0, 3))
        {
            case 0:
                questList.Add(pokajiPlzGde);
                doImen = true;
                break;
            case 1:
                questList.Add(NajmiPalchikomNa);
                doImen = false;
                break;
            case 2:
                questList.Add(ASeychasPokajiGde);
                doImen = true;
                break;
        }

        questList.Add(fruktRight.frukt._isFemineRod ? fruktRight.color._archiRodPadezj : fruktRight.color._archiImenPadezj);
        questList.Add(doImen ?  fruktRight.frukt._archiImenPadezj : fruktRight.frukt._archiRodPadezj );
        return questList;
    }

    void ClickButton(int id)
    {
        AFKTimer.ping();
        if (searchedFrukt != null)
            if (currentList[id] == searchedFrukt)
            {
                _buttonPSs[id].Play();


                searchedFrukt = null;
                //isFirstFrukt = false;
                currentItteration++;
                //AudioController.PlaySound_static(CreatePraiseAudio()); AddSoundAndPlay_Static
                AudioController.ClearQueue_Static();
                List<AudioClip> praiseAudio = CreatePraiseAudio();
                AudioController.AddSoundAndPlay_Static(praiseAudio);

                if (_doKeepObjectsEveryRound)
                {
                    _buttons[id].transform.DORewind();
                    _buttons[id].transform.DOShakeScale(0.35f, 0.85f, 9, 12, true);

                    if (currentItteration >= 3)
                    {
                        CornerController.HideCorner(() => {
                            bounceButtons(false).onComplete += () =>
                            {
                                Sequence addedInEndInterval = DOTween.Sequence();
                                addedInEndInterval.AppendInterval(praiseAudio[0].length).onComplete += Done;
                                CornerController.SetVisible(0);
                            };
                        });
                        
                    }
                    else
                    {
                        NewGame();
                    }
                }
                else
                {
                    bounceButtons(false).onComplete += () =>
                    {
                        if (currentItteration >= 3)
                        {
                            CornerController.HideCorner(() => {
                                bounceButtons(false).onComplete += () =>
                                {
                                    Sequence addedInEndInterval = DOTween.Sequence();
                                    addedInEndInterval.AppendInterval(praiseAudio[0].length).onComplete += Done;
                                    CornerController.SetVisible(0);
                                };
                            });
                        }
                        else
                        {
                            NewGame();
                        }
                    };
                }
            }   
            else
            {
                _buttons[id].transform.DORewind();
                _buttons[id].transform.DOShakeScale(0.35f, 0.85f, 9, 12, true);
                //AudioController.PlaySound_static(createWrongAudio(currentList[id], searchedFrukt)); 
                AudioController.AddSoundAndPlay_Static(createWrongAudio(currentList[id], searchedFrukt));
            }
    }
    
    
    
    
    
    public override void Execute()
    {
        InitGame();
        NewGame();
    }

    public override void ResetCommand()
    {
        DOTween.KillAll();
        HidePanel();
        AFKTimer.StopTimer_Static();
    }

    public override void Stop()
    {
        AudioController.StopAudio_static();
    }

    public override void Done()
    {
        AFKTimer.StopTimer_Static();
        HidePanel();
        
        base.Done();
    }
}
