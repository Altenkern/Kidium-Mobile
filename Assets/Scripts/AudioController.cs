using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public float speeed = 1;
    public AudioSource audioSource;

    Queue<AudioClip> clipQueue = new Queue<AudioClip>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (Instance == null)
            Instance = this;
    }
    
    IEnumerator playListOfSounds(List<AudioClip> audios)
    {
        for (int i = 0; i < audios.Count; i++)
        {
            audioSource.clip = audios[i];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    IEnumerator playNextSound()
    {
        if (clipQueue.Count > 0)
        {
            audioSource.clip = clipQueue.Dequeue();
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            yield return StartCoroutine(playNextSound());
        }
        else
            yield return null;
    }
    void AddSoundAndPlay(List<AudioClip> audios)
    {
        bool isEmpty = clipQueue.Count == 0;

        for(int i = 0; i < audios.Count; i++)
        {
            clipQueue.Enqueue(audios[i]);
        }

        if(isEmpty)
        {
            //StartCoroutine(playNextSound());
        }

    }    

    void PlaySound(List<AudioClip> audios)
    {
        StopAudio();
        StartCoroutine(playListOfSounds(audios));
    }
    
    void StopAudio()
    {
        ClearQueue();
        StopAllCoroutines();
    }

    void ClearQueue()
    {
        clipQueue.Clear();
    }    
    
    public  static  void PlaySound_static(List<AudioClip> audios)
    {
        Instance.PlaySound(audios);
    }
    public  static  void StopAudio_static()
    {
        Instance.StopAudio();
    }

    public static void AddSoundAndPlay_Static(List<AudioClip> audios)
    {
        Instance.AddSoundAndPlay(audios);
    }
    public static void ClearQueue_Static()
    {
        Instance.ClearQueue(); 
    }    
    public void Update()
    {
        audioSource.pitch = speeed;
        
        if (Time.timeScale == 0)
            audioSource.Pause();
        else
            audioSource.UnPause();

        if (audioSource.isPlaying == false && clipQueue.Count > 0)
        {
            audioSource.clip = clipQueue.Dequeue();
            audioSource.Play();
        }
    }
}
