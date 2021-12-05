
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AFKTimer : MonoBehaviour
{
    private static AFKTimer Instance;
    public List<AudioClip> AFKphrases;

    public float checkEveryNsec = 20;
    
    private bool isActive = false;
    private float timeOfLastActive;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

#if UNITY_EDITOR
        checkEveryNsec = 999;
#endif
    }
    void setAFKPhrases(List<AudioClip> clips)
    {
        AFKphrases = clips;
    }
    public static void SetAFKPhrases_Static(List<AudioClip> clips)
    {
        Instance.setAFKPhrases(clips);
    }
    public static void ping()
    {
        Instance.timeOfLastActive = Time.time;
    }

    void StartTimer()
    {
        isActive = true;
        timeOfLastActive = Time.time;
    }

    public static void StartTimer_Static()
    {
        Instance.StartTimer();
    }
    
    void StopTimer()
    {
        isActive = false;
        timeOfLastActive = Time.time;
    }

    public static void StopTimer_Static()
    {
        Instance.StopTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            if (Time.time - timeOfLastActive > checkEveryNsec)
            {
                timeOfLastActive = Time.time;
                List<AudioClip> list = new List<AudioClip>();
                list.Add(AFKphrases[UnityEngine.Random.Range(0, AFKphrases.Count)]);
                AudioController.PlaySound_static(list);
            }
    }
}
