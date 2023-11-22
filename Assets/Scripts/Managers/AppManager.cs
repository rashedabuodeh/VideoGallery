using System.Collections;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    #region Public Members 
    public static AppManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AppManager>();
            }

            return _instance;
        }
        private set { _instance = value; }
    }

    #endregion

    #region Private Members 
    private static AppManager _instance;

    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        UDPServer.OnChooseVideoMessage += OnChooseVideo;
    }

    #endregion

    #region Private Methods
    private IEnumerator WaitThenSendData(float waitTime, string videoName)
    {
        yield return new WaitForSeconds(waitTime);
        UDPServer.StartSendingData(videoName);
    }

    private void OnChooseVideo(string videoName)
    {
        Debug.Log("the chosen video is the "+ videoName);
    }

    #endregion

    #region public Methods
    public void OnDropVideo(int index)
    {
        Application.OpenURL(Application.streamingAssetsPath + "/" + "SecondApplication" + ".exe");
        string videoName;
        switch (index)
        {
            case 1:
                videoName = "First";
                break;
            case 2:
                videoName = "Second";
                break;
            case 3:
                videoName = "Third";
                break;

            default:
                videoName = "First";
                break;
        }
        StartCoroutine(WaitThenSendData(1f, videoName));
    }

    #endregion

}
