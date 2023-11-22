using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    #region Private Members 
    private VideoPlayer _myVideoPlayer;
    private string _chosenVideoName;
    private bool _isPlaying;
    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        _myVideoPlayer = GetComponent<VideoPlayer>(); 
        UDPClient.OnVideoNameMessage += OnVideoNameMessage;

    }
    private void Update()
    {
        if (_isPlaying)
            SetVideoUrl();
    }
    #endregion

    #region Private Methods
    private void SetVideoUrl()
    {
        string videoUrl = Application.streamingAssetsPath + "/" + _chosenVideoName + ".mp4";
        _myVideoPlayer.url = videoUrl;

        _myVideoPlayer.Play();
        _isPlaying = false;
    }
    #endregion

    #region public Methods
    // This Method Called by the client, and then a boolean is changed to be called in update, so it can executed on Unity's main thread to set video url
    public void OnVideoNameMessage( string videoName)
    {
        _chosenVideoName = videoName;
        _isPlaying = true;
    }
    #endregion
}