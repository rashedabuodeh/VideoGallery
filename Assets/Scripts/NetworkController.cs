using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour
{
    [SerializeField] private Text messageText;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        Close();

        serverButton.onClick.RemoveAllListeners();
        serverButton.onClick.AddListener(StartServer);

        clientButton.onClick.RemoveAllListeners();
        clientButton.onClick.AddListener(StartClient);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        DisposeActive();
    }

    private void StartServer()
    {
        serverButton.interactable = false;
        clientButton.interactable = false;
        closeButton.interactable = true;

        UDPServer.OnMessage += OnMessage;
        UDPServer.Start();
    }

    private void StartClient()
    {
        serverButton.interactable = false;
        clientButton.interactable = false;
        closeButton.interactable = true;

        UDPClient.OnMessage += OnMessage;
        UDPClient.Start();
    }

    private void Close()
    {
        serverButton.interactable = true;
        clientButton.interactable = true;
        closeButton.interactable = false;

        messageText.text = "";

        DisposeActive();
    }

    private void DisposeActive()
    {
        if (UDPServer.active)
            UDPServer.Dispose();

        if (UDPClient.active)
            UDPClient.Dispose();
    }

    private void OnMessage(string message)
    {
        //messageText.text += $"\n{message}";
    }
}
