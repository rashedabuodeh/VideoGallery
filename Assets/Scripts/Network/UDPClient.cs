using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class UDPClient
{
    #region Public Members 
    public static bool Active = false;
    public static UnityEngine.Events.UnityAction<string> OnVideoNameMessage;

    #endregion

    #region Private Members 
    private static UdpClient _udpClient;
    private static IPEndPoint remoteEndPoint;

    #endregion


    #region Private Methods
    private static void StartUDPClient(string ipAddress, int port)
    {
        _udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        _udpClient.Connect(remoteEndPoint);

        // Start receiving data asynchronously
        _udpClient.BeginReceive(ReceiveData, null);

        // Send a message to the server
        SendData("Hello, server!");

    }

    private static void ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = _udpClient.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

        Debug.Log("Received from server: " + receivedMessage);

        OnVideoNameMessage?.Invoke(receivedMessage);

        // Continue receiving data asynchronously
        _udpClient.BeginReceive(ReceiveData, null);
    }
    private static void SendData(string message)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Send the message to the server
        _udpClient.Send(sendBytes, sendBytes.Length);

        Debug.Log("Sent to server: " + message);
    }
    #endregion

    #region public Methods
    public static void Start()
    {
        // Start the UDP client and connect to the remote server
        StartUDPClient("127.0.0.1", UDPServer.SERVER_PORT);
        Active = true;
    }

    public static void Dispose()
    {
        Active = false;
        if (_udpClient != null)
        {
            _udpClient.Dispose();
            _udpClient = null;
        }

        OnVideoNameMessage = null;
    }

    #endregion
    
}
