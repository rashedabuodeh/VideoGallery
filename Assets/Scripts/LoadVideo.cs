using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    void Start()
    {
        string videoUrl = Application.streamingAssetsPath + "/" + "video_streamingAssets1" + ".mp4";
        myVideoPlayer.url = videoUrl;
    }
}