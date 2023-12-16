using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public class Client : MonoBehaviour
{
    private UdpClient m_Client;
    public string m_Ip = "192.168.64.2";
    public int m_Port = 12345;
    public ToServerPacket m_SendPacket = new ToServerPacket();
    public ToClientPacket m_ReceivePacket = new ToClientPacket();
    private IPEndPoint m_RemoteIpEndPoint;

    void Start()
    {
        Application.targetFrameRate = 60;
        InitClient();
    }

    void Update()
    {
        Send();
        Receive();
        
    }

    void OnApplicationQuit()
    {
        CloseClient();
    }

    void InitClient()
    {
        m_Client = new UdpClient();
        m_Client.Client.Blocking = false;
        m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        // SendPacket에 배열이 있으면 선언 해 주어야 함.
        m_SendPacket.m_IntArray = new int[2];
    }

    void SetSendPacket()
    {
        m_SendPacket.m_BoolVariable = true;
        m_SendPacket.m_IntVariable = 13;
        m_SendPacket.m_IntArray[0] = 7;
        m_SendPacket.m_IntArray[1] = 47;
        m_SendPacket.m_FloatlVariable = 2020;
        m_SendPacket.m_StringlVariable = "Coder Zero";
    }

    void Send()
    {
        try
        {
            SetSendPacket();
            byte[] bytes = StructToByteArray(m_SendPacket);
            m_Client.Send(bytes, bytes.Length, m_Ip, m_Port);
            Debug.Log($"[Send] {m_Ip}:{m_Port} Size : {bytes.Length} byte");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }
    }

    void Receive()
    {
        try
        {
            byte[] bytes = m_Client.Receive(ref m_RemoteIpEndPoint);
            Debug.Log($"[Receive] Remote IpEndPoint : {m_RemoteIpEndPoint.ToString()} Size : {bytes.Length} byte");
            m_ReceivePacket = ByteArrayToStruct<ToClientPacket>(bytes);
            DoReceivePacket(); // 받은 값 처리
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }

    }

    void DoReceivePacket()
    {
        Debug.LogFormat($"BoolVariable = {m_ReceivePacket.m_BoolVariable} " +
            $"IntlVariable = {m_ReceivePacket.m_IntVariable} " +
            $"m_IntArray[0] = {m_ReceivePacket.m_IntArray[0]} " +
            $"m_IntArray[1] = {m_ReceivePacket.m_IntArray[1] } " +
            $"FloatlVariable = {m_ReceivePacket.m_FloatlVariable} " +
            $"StringlVariable = {m_ReceivePacket.m_StringlVariable}");
        //출력: BoolVariable = True IntlVariable = 13 m_IntArray[0] = 7 m_IntArray[1] = 47 FloatlVariable = 2020 StringlVariable = Coder Zero
    }

    void CloseClient()
    {
        m_Client.Close();
    }

    byte[] StructToByteArray(object obj)
    {
        int size = Marshal.SizeOf(obj);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    T ByteArrayToStruct<T>(byte[] buffer) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        if (size > buffer.Length)
        {
            throw new Exception();
        }

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buffer, 0, ptr, size);
        T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return obj;
    }
}
