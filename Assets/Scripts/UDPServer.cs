using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPServer : MonoBehaviour
{
    private UdpClient udpServer;
    private IPEndPoint remoteEndPoint;

    private void Start()
    {
        // Example: Start the UDP server on port 5555
        StartUDPServer(5555);
    }

    private void StartUDPServer(int port)
    {
        udpServer = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        Debug.Log("Server started. Waiting for messages...");

        // Start receiving data asynchronously
        udpServer.BeginReceive(ReceiveData, null);
    }

    private void ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = udpServer.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);

        Debug.Log("Received from client: " + receivedMessage);

        // Process the received data

        // Continue receiving data asynchronously
        udpServer.BeginReceive(ReceiveData, null);
    }

    private void SendData(string message, IPEndPoint endPoint)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Send the message to the client
        udpServer.Send(sendBytes, sendBytes.Length, endPoint);

        Debug.Log("Sent to client: " + message);
    }
}