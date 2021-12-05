using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private bool doLoadScene = false;
    private void Awake()
    {
        doLoadScene = false;
        PlayerPrefs.SetString("CurrentLevel", "2 FruAndVeg");
        //StartCoroutine(LoadSceneAsync());
    }

    public void OnDropChange()
    {
        switch(dropdown.value)
        {
            case 0:
                PlayerPrefs.SetString("CurrentLevel", "2 FruAndVeg");
                break;
            case 1:
                PlayerPrefs.SetString("CurrentLevel", "3 Animals");
                break;
            case 2:
                PlayerPrefs.SetString("CurrentLevel", "4 Sea");
                break;
            case 3:
                PlayerPrefs.SetString("CurrentLevel", "5 Birds");
                break;
            default:
                PlayerPrefs.SetString("CurrentLevel", "2 FruAndVeg");
                break;
        }
    }

    public void LoadScene()
    {
        doLoadScene = true;
        //SceneManager.LoadScene(1);
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
               // m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (doLoadScene)
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }
            else
            {
                Debug.Log("Pro :" + asyncOperation.progress);
            }

            yield return null;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }
}
