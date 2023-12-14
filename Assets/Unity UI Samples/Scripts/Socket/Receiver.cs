using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Receiver : MonoBehaviour
{
    private UdpClient receiver;
    public int port = 12345;
    public string message;

    void Awake()
    {
        InitReceiver();
    }

    void OnApplicationQuit()
    {
        CloseReceiver();
    }

    void InitReceiver()
    {
        try {
            if (null == receiver) {
                receiver = new UdpClient(port);
                receiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
        } catch (SocketException e) {
            Debug.Log(e.Message);
        }
    }

    void ReceiveCallback(IAsyncResult ar)
    {
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);

        byte[] received;
        if (receiver != null) {
            received = receiver.EndReceive(ar, ref endpoint);
        } else {
            return;
        }

        receiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);

        message = Encoding.Default.GetString(received);
        message = message.Trim();

        DoReceive();
    }

    void DoReceive()
    {
        Debug.Log(message);
    }

    void CloseReceiver()
    {
        if (receiver != null) {
            receiver.Close();
            receiver = null;
        }
    }
}
