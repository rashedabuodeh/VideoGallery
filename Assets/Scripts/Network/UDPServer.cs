using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class UDPServer
{
    public const int SERVER_PORT = 5555;

    public static bool active = false;
    public static UnityEngine.Events.UnityAction<string> OnMessage;

    private static UdpClient udpServer;
    private static IPEndPoint remoteEndPoint;

    public static void Start()
    {
        // Start the UDP server on port SERVER_PORT
        StartUDPServer(SERVER_PORT);
        active = true;
    }

    private static void StartUDPServer(int port)
    {
        udpServer = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        Debug.Log("Server started. Waiting for messages...");
        OnMessage?.Invoke("Server started. Waiting for messages...");

        // Start receiving data asynchronously
        udpServer.BeginReceive(ReceiveData, null);
    }

    private static void ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = udpServer.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

        Debug.Log($"Received from client \"{remoteEndPoint}\": {receivedMessage}");
        OnMessage?.Invoke($"Received from client \"{remoteEndPoint}\": {receivedMessage}");

        // Process the received data
        switch (receivedMessage)
        {
            case "Hello, server!":
                SendData("Hello, client!", remoteEndPoint);
                break;
        }

        // Continue receiving data asynchronously
        udpServer.BeginReceive(ReceiveData, null);
    }

    private static void SendData(string message, IPEndPoint endPoint)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Send the message to the client
        udpServer.Send(sendBytes, sendBytes.Length, endPoint);

        Debug.Log($"Sent to client \"{endPoint}\": {message}");
        OnMessage?.Invoke($"Sent to client \"{endPoint}\": {message}");
    }
    public static void Dispose()
    {
        active = false;

        if (udpServer != null)
        {
            udpServer.Dispose();
            udpServer = null;
        }

        OnMessage = null;
    }
}