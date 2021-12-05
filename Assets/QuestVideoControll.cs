using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class QuestVideoControll : MonoBehaviour
{
    [SerializeField] string startVideo, endVideo;
    [SerializeField] Quest firstQuest;
    [SerializeField] GameObject EndGameGO;
    [SerializeField] float timeOf1stVideoEnd, timeOf2ndVideoEnd;
    [SerializeField] AudioSource audiosource;
    VideoPlayer player;
    CanvasGroup canvasGroup;

    [SerializeField] float fadeSpeed = 0.3f;

    bool is1sPlayed = false;
    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Play1stVideo();
    }

    //private void OnEnable()
    //{
    //    player.loopPointReached += EndedPlay;
    //}
    //private void OnDisable()
    //{
    //    player.loopPointReached -= EndedPlay;
    //}

    public void Play1stVideo()
    {
        player.url = Application.streamingAssetsPath + "/" + startVideo;
        StartCoroutine(PlayVideo());
        StartCoroutine(EndFirst());
    }

    public void Play2ndVideo()
    {
        player.url = Application.streamingAssetsPath + "/" + endVideo;
        StartCoroutine(PlayVideo());
        StartCoroutine(EndSecond());
    }

    IEnumerator EndFirst()
    {
        float timestamp = Time.time;

        while (timestamp + timeOf1stVideoEnd > Time.time)
            yield return null;

        EndedPlay(true);
    }

    IEnumerator EndSecond()
    {
        float timestamp = Time.time;

        while (timestamp + timeOf2ndVideoEnd > Time.time)
            yield return null;

        EndedPlay(false);
    }


    void EndedPlay(bool is1)
    {
        StartCoroutine(DelayedFade(0));
        player.Stop();
        
        if(!is1sPlayed)
        {
            is1sPlayed = true;
            firstQuest.StartQuest();
        }
        else
        {
            EndGameGO.SetActive(true);
        }
    }
    IEnumerator DelayedFade(float targetAlpha)
    {
        //yield return new WaitForSeconds(1f);
        if (targetAlpha != 0)
            while (canvasGroup.alpha != targetAlpha)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
                yield return null;
            }
        else
            canvasGroup.alpha = 0;
    }

    IEnumerator PlayVideo()
    {
        StartCoroutine(DelayedFade(1));
        player.Prepare();
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.SetTargetAudioSource(0, audiosource);
        player.EnableAudioTrack(0, true);

        while (!player.isPrepared)

            yield return null;
        canvasGroup.alpha = 1;


        player.Play();

    }
}
