using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class UDPServer
{
    #region Public Members 

    public const int SERVER_PORT = 5555;

    public static bool Active = false;
    public static UnityEngine.Events.UnityAction<string> OnChooseVideoMessage;

    #endregion

    #region Private Members 

    private static UdpClient _udpServer;
    private static IPEndPoint remoteEndPoint;

    #endregion


    #region Private Methods
    private static void StartUDPServer(int port)
    {
        _udpServer = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Parse ("127.0.0.1"), port);

        Debug.Log("Server started");
        _udpServer.BeginReceive(ReceiveData, null);
    }
    private static void ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = _udpServer.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

        Debug.Log($"Received from client \"{remoteEndPoint}\": {receivedMessage}");


        // Continue receiving data asynchronously
        _udpServer.BeginReceive(ReceiveData, null);
    }
    private static void SendData(string message, IPEndPoint endPoint, bool welcomeing = false)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Send the message to the client
        _udpServer.Send(sendBytes, sendBytes.Length, endPoint);

        Debug.Log($"Sent to client \"{endPoint}\": {message}");
        if(!welcomeing)
            OnChooseVideoMessage?.Invoke(message);
    }
    #endregion

    #region public Methods
    public static void Start()
    {
        // Start the UDP server on port SERVER_PORT
        StartUDPServer(SERVER_PORT);
        Active = true;
    }


    public static void StartSendingData(string message)
    {
        SendData(message, remoteEndPoint);
    }
    public static void Dispose()
    {
        Active = false;

        if (_udpServer != null)
        {
            _udpServer.Dispose();
            _udpServer = null;
        }

        OnChooseVideoMessage = null;
    }
    #endregion
}
