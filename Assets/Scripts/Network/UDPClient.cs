using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
public static class UDPClient
{
    public static bool active = false;
    public static UnityEngine.Events.UnityAction<string> OnMessage;

    private static UdpClient udpClient;
    private static IPEndPoint remoteEndPoint;

    public static void Start()
    {
        // Example: Start the UDP client and connect to the remote server
        StartUDPClient("127.0.0.1", UDPServer.SERVER_PORT);
        active = true;
    }

    public static void Dispose()
    {
        active = false;
        if (udpClient != null)
        {
            udpClient.Dispose();
            udpClient = null;
        }

        OnMessage = null;
    }

    private static void StartUDPClient(string ipAddress, int port)
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        udpClient.Connect(remoteEndPoint);

        // Start receiving data asynchronously
        udpClient.BeginReceive(ReceiveData, null);

        // Send a message to the server
        SendData("Hello, server!");
    }

    private static void ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = udpClient.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

        Debug.Log("Received from server: " + receivedMessage);
        OnMessage?.Invoke("Received from server: " + receivedMessage);

        // Continue receiving data asynchronously
        udpClient.BeginReceive(ReceiveData, null);
    }

    private static void SendData(string message)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Send the message to the server
        udpClient.Send(sendBytes, sendBytes.Length);

        Debug.Log("Sent to server: " + message);
        OnMessage?.Invoke("Sent to server: " + message);
    }
}