using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public static VideoController Instance;

    public VideoPlayer videoSource;

    string fileURL = "";
    static string newFileURL = "";

    private void Awake()
    {
        videoSource = GetComponent<VideoPlayer>();

        if (Instance == null)
            Instance = this;
    }


    void StopVideo()
    {
        videoSource.Stop();
    }
    void PlayVideo(string videoURL, bool isInFile, bool isInterner = false)
    {
        StopVideo();
        if (!isInterner)
        {
            string url;
            if (isInFile)
            {
                url = System.IO.Path.Combine(Application.streamingAssetsPath, videoURL);
            }
            else
            {
                url = videoURL;
            }
            videoSource.url = url;
            videoSource.Play();
        }
        else
        {
            videoSource.url = videoURL;
            videoSource.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoSource.EnableAudioTrack(0, true);
            videoSource.Prepare();
            videoSource.Play();
        }
    }

    public VideoPlayer getVideoPlayer()
    {
        return videoSource;
    }
    public static void StopVideo_Static()
    {
        Instance.StopVideo();
    }
    public static void PlayVideo_Static(string videoURL, bool isInternet)
    {
        if (!isInternet)
        {
            Instance.StartCoroutine(videoURLPath(videoURL));
        }
        else
        {
            Instance.PlayVideo(videoURL, false, isInternet);
        }
    }
    public static VideoPlayer GetVideoPlayer_Static()
    {
        return Instance.getVideoPlayer();
    }

    private void Update()
    {
        videoSource.playbackSpeed = Time.timeScale == 0 ? 0 : 1;

    }


    static IEnumerator videoURLPath(string str)
    {
        print("Stream asset path = " + str);

        if (str.Contains("://") || str.Contains(":///"))
        {
            WWW www = new WWW(str);
            yield return www;
            newFileURL = www.text;
            
            print("New file URL =" + newFileURL);

            Instance.PlayVideo(newFileURL, false);
        }
        else
        {
            newFileURL = str;

            print("New file URL =" + newFileURL);

            Instance.PlayVideo(newFileURL, true);

        }
    }
}
