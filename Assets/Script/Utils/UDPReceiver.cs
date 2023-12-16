using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;
    private Thread receiveThread;
    private bool isRunning = true;

    private void Start()
    {
        int port = 12345; // 수신 포트
        udpClient = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        while (isRunning)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string text = Encoding.UTF8.GetString(data);
                
                // 여기에서 수신된 데이터를 처리합니다.
                Debug.Log("Received Data: " + text);
            }
            catch (Exception e)
            {
                Debug.LogError("Error: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        udpClient.Close();
        receiveThread.Abort();
    }
}
