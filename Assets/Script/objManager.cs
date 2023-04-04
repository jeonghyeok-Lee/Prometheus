using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class objManager : MonoBehaviour
{
    public GameObject point;    // 하나의 점 역할을 수행할 객체
   
    /// <summary>
    /// 
    /// </summary>
    private float[] angle;         // 각도
    private float[] distance;       // 거리
    private float[] quality;        // 품질

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
        
        for(int a = 0; a<800; a++)
        {
            GameObject instance = Instantiate(point);
            /*            Debug.Log(distance[a] + " " + distance[a] * Math.Sin(angle[a]));*/
            float x = distance[a] * Mathf.Cos(angle[a] * (Mathf.PI / 180.0f));
            float y = distance[a] * Mathf.Sin(angle[a] * (Mathf.PI / 180.0f));
            instance.transform.position = new Vector3((float)x, 0, -(float)y);

            Debug.Log($"{x},{y}");
            
        }

/*        Debug.Log($"y = {distance[0]} * {Math.Sin(angle[0] * Math.PI / 180.0)} = {distance[0]* Math.Sin(angle[0] * Math.PI / 180.0)}");
        Debug.Log(3 * Math.Sin(30 * Math.PI / 180));
        Debug.Log(3 * Math.Cos(30 * Math.PI / 180));*/


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
