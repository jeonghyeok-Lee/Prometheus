using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public class Server : MonoBehaviour
{
    private UdpClient m_Server;
    public string m_Ip = "127.0.0.1";
    public int m_Port = 12345;
    private IPEndPoint m_RemoteIpEndPoint;
    public ToServerPacket m_ReceivePacket = new ToServerPacket();
    public ToClientPacket m_SendPacket = new ToClientPacket();

    void Start()
    {
        Application.targetFrameRate = 60;
        InitServer();
    }

    void Update()
    {
        Send();
        Receive();
    }

    void OnApplicationQuit()
    {
        CloseServer();
    }

    void InitServer()
    {
        m_Server = new UdpClient(m_Port);
        m_Server.Client.Blocking = false;
        m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        // SendPacket에 배열이 있으면 선언 해 주어야 함.
        m_SendPacket.m_IntArray = new int[2];
    }

    void Receive()
    {
        try
        {
            byte[] bytes = m_Server.Receive(ref m_RemoteIpEndPoint);
            Debug.Log($"[Receive] Remote IpEndPoint : {m_RemoteIpEndPoint.ToString()} Size : {bytes.Length} byte");
            m_ReceivePacket = ByteArrayToStruct<ToServerPacket>(bytes);
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
        Debug.LogFormat($"m_IntArray[0] = {m_ReceivePacket.m_IntArray[0]} " +
            $"m_IntArray[1] = {m_ReceivePacket.m_IntArray[1] } " +
            $"FloatlVariable = {m_ReceivePacket.m_FloatlVariable} " +
            $"StringlVariable = {m_ReceivePacket.m_StringlVariable} " +
            $"BoolVariable = {m_ReceivePacket.m_BoolVariable} " +
            $"IntlVariable = {m_ReceivePacket.m_IntVariable} ");
        //출력: m_IntArray[0] = 7 m_IntArray[1] = 47 FloatlVariable = 2020 StringlVariable = Coder Zero BoolVariable = True IntlVariable = 13 
    }

    void Send()
    {
        try
        {
            SetSendPacket();
            byte[] bytes = StructToByteArray(m_SendPacket);
            m_Server.Send(bytes, bytes.Length, m_RemoteIpEndPoint);
            Debug.Log($"[Send] Remote IpEndPoint : {m_RemoteIpEndPoint.ToString()} Size : {bytes.Length} byte");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }
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

    void CloseServer()
    {
        m_Server.Close();
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
