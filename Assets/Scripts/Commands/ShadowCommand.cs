
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[CreateAssetMenu(fileName = "ShadowCommand", menuName = "Scenery/Scenes/Shadow Command", order = 3)]
public class ShadowCommand : Command
{
    [SerializeField] private bool _doKeepObjectsEveryRound = false;
    
    [SerializeField] private List<Frukt> vegetables;

    private List<Frukt> currentList, fructsThatWereSearched;

    private Frukt searchedFrukt;
    
    //leftest, left, mid, right, rightest buttons
    [HideInInspector] public Button llBut, lBut, midBut, rBut, rrBut;
    
    private Button[] _buttons = new Button[5];
    private ParticleSystem[] _buttonPSs = new ParticleSystem[5];
    

    private GameObject mainPanel;
    
    
    [SerializeField] private List<AudioClip> AFKphrases, rightPhraes;

    [SerializeField] private AudioClip Eto, Naidi, pokajiPlzGde, NajmiPalchikomNa, ASeychasPokajiGde;

    [SerializeField] private AudioClip[] shadowAudios;


    private Image shadowSprite, cornerSprite;
    private int currentItteration;
    
    void InitGame()
    {
        //init game data
        currentItteration = 0;
        fructsThatWereSearched = new List<Frukt>();
        currentList = new List<Frukt>();
        //isFirstFrukt = true;
        initButtons();
        RevealPanel();
        AFKTimer.SetAFKPhrases_Static(AFKphrases);
        AFKTimer.StartTimer_Static();
        
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

    void NewGame()
    {
        InitCurrentList();
        if(currentItteration == 0 || _doKeepObjectsEveryRound)
            RefreshButtonSprites();

        if(_doKeepObjectsEveryRound)
        {
            if (currentItteration == 0)
                bounceButtons(true).onComplete += () => { CornerController.MakeCorner(() => { AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt)); }); };
            else
                AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt));
        }
        else
            bounceButtons(true).onComplete += () => { CornerController.MakeCorner(() => { AudioController.AddSoundAndPlay_Static(createQuestAudio(searchedFrukt)); }); };

        CornerController.SetVisible(1);
    }


    void initButtons()
    {
        //init main panel
        mainPanel = ShadowPanelControll.static_getPanel();
        
        //init buttons
        List<UnityEngine.Object> butt = ShadowPanelControll.GetObjects_Static();
        
        llBut  = (Button)butt[0];
        lBut   = (Button)butt[1];
        midBut = (Button)butt[2];
        rBut   = (Button)butt[3];
        rrBut  = (Button)butt[4];
        shadowSprite = (Image)butt[5];
        cornerSprite = (Image)butt[6];
        
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

    void InitCurrentList()
    {
        //init vegetables list
        if (currentList.Count != 5 || currentItteration == 0 || !_doKeepObjectsEveryRound)
        {
            currentList = vegetables.OrderBy(x => Random.Range(0, 1000)).Take(5).ToList();
        }

        if (_doKeepObjectsEveryRound)
        {

            if (fructsThatWereSearched.Count < 5)
            {
                searchedFrukt = getNotSearchedFromCurrentList();
                fructsThatWereSearched.Add(searchedFrukt);
            }
            else
            {
                Debug.LogError("Overcapacty of object at game");
                fructsThatWereSearched.Clear();
                InitCurrentList();
            }
        }
        else
        {
            currentList = vegetables.OrderBy(x => Random.Range(0, 1000)).Take(5).ToList();
            searchedFrukt = currentList[Random.Range(0, 5)];
            fructsThatWereSearched.Add(searchedFrukt);
        }
        shadowSprite.sprite = searchedFrukt._shadow;
        Debug.Log(searchedFrukt._name + " is searched vegetable");                                
    }

    Frukt getNotSearchedFromCurrentList()
    {
        Frukt fru = currentList[Random.Range(0, 5)];
        return fructsThatWereSearched.Contains(fru) ? getNotSearchedFromCurrentList() : fru;
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
            bounceSequence.Insert(0, shadowSprite.transform.DOScale(1f, 1.2f));
        }
        else
        {
            bounceSequence.Insert(0, shadowSprite.transform.DOScale(0f, 1.2f));
        }
        if (doAdds)
        {
            if (inOut)
            {
                bounceSequence.Insert(0, cornerSprite.DOFade(1f, 1.2f));
            }
            else
            {
                bounceSequence.Insert(0, cornerSprite.DOFade(0f, 1.2f));
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
            if (currentItteration == 0 || !_doKeepObjectsEveryRound)
            //set sprites of buttons;
                for (int i = 0; i < 5; i++)
                {
                    _buttons[i].image.sprite = currentList[i]._sprites[Random.Range(0, currentList[i]._sprites.Length)];
                    _buttons[i].image.SetNativeSize();
                }
        }
    }

    List<AudioClip> createWrongAudio(Frukt fruktWrong, Frukt fruktRight)
    {
        List<AudioClip> wrongList = new List<AudioClip>();

        wrongList.Add(Eto);
        wrongList.Add(fruktWrong._bonyImenPadezj);
        //wrongList.Add(Naidi);
        //if (fruktRight._bonyRodPadezj != null)
        //{
        //    wrongList.Add(fruktRight._bonyRodPadezj);
        //}
        //else
        //{
        //    wrongList.Add(fruktRight._bonyImenPadezj);
        //}

        return wrongList;
    }
    List<AudioClip> CreatePraiseAudio()
    {
        List<AudioClip> questList = new List<AudioClip>();
        questList.Add(rightPhraes[Random.Range(0, rightPhraes.Count)]);
        return questList;
    }
    List<AudioClip> createQuestAudio(Frukt fruktRight)
    {
        List<AudioClip> questList = new List<AudioClip>();

        questList.Add(shadowAudios[Random.Range(0, shadowAudios.Count())]);
        
        //bool doImen = true;
        
        //switch (Random.Range(0,3))
        //{
        //    case 0:
        //        questList.Add(pokajiPlzGde);
        //        doImen = true;
        //        break;
        //    case 1:
        //        questList.Add(NajmiPalchikomNa);
        //        doImen = false;
        //        break;
        //    case 2:        
        //        questList.Add(ASeychasPokajiGde);
        //        doImen = true;
        //        break;
        //}
        //questList.Add(!doImen ? fruktRight._bonyRodPadezj : fruktRight._bonyImenPadezj);
        return questList;
    }

    void ClickButton(int id)
    {
        AFKTimer.ping();
        if(searchedFrukt!= null)
            if (currentList[id] == searchedFrukt)
            {
                searchedFrukt = null;
                _buttonPSs[id].Play();
                     
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
                        CornerController.HideCorner(() =>
                        {
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
                    CornerController.HideCorner(() =>
                    {
                        bounceButtons(false).onComplete += () =>
                    {
                        if (currentItteration >= 3)
                        {
                            Sequence addedInEndInterval = DOTween.Sequence();
                            addedInEndInterval.AppendInterval(praiseAudio[0].length).onComplete += Done;
                            CornerController.SetVisible(0);
                        }
                        else
                        {
                            NewGame();
                        }
                    };
                    });
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
