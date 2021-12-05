using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class ScenarioController : MonoBehaviour
{
    delegate void EndAction();
    event EndAction commandEnded;

    public bool doTestScenario = false;
    public Scenario _2, _3, _4, _5, _TestScenario;

    public static ScenarioController Instance;
   
    public Scenario mainScenario;
    public GameObject ImageForVideo;


    public UnityEngine.UI.Image backgroundImageOfVideo;
    
    

    private int currentState = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GetCurrentLvl();





        mainScenario.InitScenario();
        
    }
    private void Start()
    {
        //videoPlayer.frame = 400;
        NextState();
    }
    //private void EndReached(VideoPlayer source)
    //{
    //    NextState();
    //}



    void GetCurrentLvl() //should be new class
    {
        switch(PlayerPrefs.GetString("CurrentLevel"))
        {
            case "2 FruAndVeg":
                mainScenario = _2;
                break;
            case "3 Animals":
                mainScenario = _3;
                break;
            case "4 Sea":
                mainScenario = _4;
                break;
            case "5 Birds":
                mainScenario = _5;
                break;
            default:
                mainScenario = _2;
                break;
        }
        if (doTestScenario)
            mainScenario = _TestScenario;
    }


    void CommandEndedEvent()
    {
        mainScenario.CommandEnded();
    }

    public static void static_CommandEndedEvent()
    {
        Instance.CommandEndedEvent();
    }

    void NextState()
    {
        currentState++;
        if (currentState == 2)
        {
            //UITransitor.static_MakeTransition();
            mainScenario.PlayNext();
        }
        else if(currentState == 3)
        {
            ExitGame();
        }
    }

    public void SetBackgroundImage(Sprite sprite)
    {
        backgroundImageOfVideo.gameObject.SetActive(true);
        backgroundImageOfVideo.sprite = sprite;
    }
    
    public static void SetBackgroundImage_Static(Sprite sprite)
    {
        Instance.SetBackgroundImage(sprite);
    }

    public void DeactiveBackgroundImage()
    {
        backgroundImageOfVideo.gameObject.SetActive(false);
    }
    public static void DeactiveBackgroundImage_Static()
    {
        Instance.DeactiveBackgroundImage();
    }
    
    public static void NextState_Static()
    {
        Instance.NextState();
    }

    public void ReplayCurrent()
    {
        mainScenario.ReplayCommand();   
    }

    public static void ReplayCurrent_Static()
    {
        Instance.ReplayCurrent();
    }    

    public void ExitGame()
    {
        Debug.Log("Game is ended");
        SceneManager.LoadScene("AfterGameScene");

    }

    //void CommandEnded()
    //{
    //    mainScenario.CommandEnded();
    //}
}
