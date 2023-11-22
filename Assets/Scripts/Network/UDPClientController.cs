using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPClientController : MonoBehaviour
{

    #region Unity CallBacks 
    private void Awake()
    {
        StartClient();
    }

    private void OnDestroy()
    {
        Close();
    }

    #endregion

    #region Private Methods
    private void StartClient()
    {
        UDPClient.Start();
    }

    private void Close()
    {
        if (UDPClient.Active)
            UDPClient.Dispose();
    }

    #endregion

}
