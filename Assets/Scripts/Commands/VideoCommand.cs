using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoCommand", menuName = "Scenery/Video Command", order = 6)]
public class VideoCommand : Command
{
    [SerializeField] private string _videoPath;
    [SerializeField] private bool _isInternet;

    private string videoClipURL;
    public override void Execute()
    {
        
        videoClipURL = _isInternet ? _videoPath : Application.streamingAssetsPath + "/" + _videoPath;
        VideoController.GetVideoPlayer_Static().loopPointReached += endReached;
        VideoController.PlayVideo_Static(videoClipURL, _isInternet);
    }

    private void endReached(VideoPlayer videoPlayer)
    {
        VideoController.GetVideoPlayer_Static().loopPointReached -= endReached;
        Done();
    }

    public override void ResetCommand()
    {
        Stop();
    }

    public override void Stop()
    {
        VideoController.GetVideoPlayer_Static().loopPointReached -= endReached;
        VideoController.StopVideo_Static();
    }

    /*IEnumerator WaitForVideoLenght()
    {

    }*/
}
