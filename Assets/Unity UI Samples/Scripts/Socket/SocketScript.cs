using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UDP 통신 테스트
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketScript : MonoBehaviour
{
    // UDP 통신 테스트
    private UdpClient udpClient;  // UDP 클라이언트 소켓
    private IPEndPoint remoteEndPoint;
    private Thread receiveThread;
    private bool isRunning = true;
    int port = 12345;  // 수신 포트
    public string serverIp = "192.168.64.2";  // IP 주소
    private string message;

    void Start()
    {
        // UDP 통신 테스트
            udpClient = new UdpClient(port);  // UDP 클라이언트 초기화
            udpClient.Connect(serverIp, port);

            remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
    }

    void Update()
    {
        Debug.Log(message);
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

    public string GetMessage()
    {
        return message;
    }

    public void SetMessage(string newMessage)
    {
        message = newMessage;
    }
}
