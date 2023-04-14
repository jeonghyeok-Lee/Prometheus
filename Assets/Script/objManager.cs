using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class objManager : MonoBehaviour
{
    public GameObject point;    // 하나의 점 역할을 수행할 객체

    private GameObject instance; // 생성한 포인트들을 가지고 있을 객체

    /// <summary>
    /// 
    /// </summary>
    private float[] angle;         // 각도
    private float[] distance;       // 거리
    private float[] quality;        // 품질

    public float limitTime = 60;
    private float currTime;
    private float nowTime = 0;


    // Start is called before the first frame update
    void Start()
    {

        FileStream test = new FileStream(@"..\Prometheus\Assets\data\3.txt", FileMode.Open);
        StreamReader streamReader = new StreamReader(test);
        string value = "";
        string[] str = { };

        int count = 867;
        int i = 0;          // 메모장의 행의 총 개수

        angle = new float[count];
        distance = new float[count];
        quality = new float[count];

        while (!streamReader.EndOfStream)
        {
            value = streamReader.ReadLine();
            if (i > 2)
            {
                str = value.Split(' ');

                angle[i-3] = float.Parse(str[0]);
                distance[i - 3] = float.Parse(str[1])*0.01f;
                quality[i - 3] = float.Parse(str[2]);

            }
            i++;
        }
        streamReader.Close();

/*        for (int a = 0; a < count; a += 2)
        {
            GameObject instance = Instantiate(point);
            Debug.Log(distance[a] + " " + distance[a] * Math.Sin(angle[a]));
            float x = distance[a] * Mathf.Cos(angle[a] * (Mathf.PI / 180.0f));
            float y = distance[a] * Mathf.Sin(angle[a] * (Mathf.PI / 180.0f));
            instance.transform.position = new Vector3((float)x, 0, -(float)y);

  *//*          Debug.Log($"{x},{y}");*//*  

        }*/


    }

    // Update is called once per frame
    void Update()
    {

        if(currTime > limitTime)        // 현재 시간이 제한 시간보다 큰 경우
        {
/*            currTime = 0;*/

        }
        else{
            currTime += Time.deltaTime;


            if (Mathf.Round(currTime) > nowTime/2)
            {
                nowTime = Mathf.Round(currTime);
                for (int i = 0; i < 867; i +=2)
                {
                    instance = Instantiate(point);
                    float x = distance[i] * Mathf.Cos(angle[i] * (Mathf.PI / 180.0f));
                    float y = distance[i] * Mathf.Sin(angle[i] * (Mathf.PI / 180.0f));
                    instance.transform.position = new Vector3((float)x, currTime, -(float)y);
                }
                Debug.Log($"{Mathf.Round(currTime)} {currTime}");
            }

        }

    }
}
