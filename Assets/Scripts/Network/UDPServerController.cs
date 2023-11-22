using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPServerController : MonoBehaviour
{
    #region Unity CallBacks 
    private void Start()
    {
        StartServer();
    }

    private void OnDestroy()
    {
        Close();
    }

    #endregion

    #region Private Methods

    private void StartServer()
    {
        UDPServer.Start();
    }

    private void Close()
    {
        if (UDPServer.Active)
            UDPServer.Dispose();
    }

    #endregion
}
