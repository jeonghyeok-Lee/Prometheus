using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Sender : MonoBehaviour
{
    private UdpClient sender = new UdpClient();
    public string receiverIp = "192.168.64.2";
    public int port = 12345;
    public string message;
    private byte[] sendBytes;

    void Start()
    {
        InitSender();
    }

    void Update()
    {
        SetSendPacket();
        DoBeginSend(sendBytes);
    }

    void OnApplicationQuit()
    {
        CloseSender();
    }

    void InitSender()
    {
        sender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        sender.Connect(IPAddress.Parse(receiverIp), port);
    }

    void SetSendPacket()
    {
        message = "UDP통신 테스트";
        sendBytes = Encoding.UTF8.GetBytes(message);
    }

    void DoBeginSend(byte[] packets)
    {
        sender.BeginSend(packets, packets.Length, new AsyncCallback(SendCallback), sender);
    }

    void SendCallback(IAsyncResult ar)
    {
        UdpClient udpClient = (UdpClient)ar.AsyncState;
    }

    void CloseSender()
    {
        if (sender != null) {
            sender.Close();
            sender = null;
        }
    }
}
