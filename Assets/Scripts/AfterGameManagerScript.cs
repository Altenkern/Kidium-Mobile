using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGameManagerScript : MonoBehaviour
{
    public void RestartClick()
    {
        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            if(PlayerPrefs.GetString("CurrentLevel") != "")
            {
                SceneManager.LoadScene(1);
            }
            else
                SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }    

    public void BackClick()
    {
        SceneManager.LoadScene(0);
    }
}
