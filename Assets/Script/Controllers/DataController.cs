using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataController : MonoBehaviour
{
    public TextAsset jsonFile;            // JSON 파일을 할당하기 위한 변수

    private PointData jsonData;
    
    private int width;      //  이미지의 가로 크기
    private int height;     //  이미지의 세로 크기


    void Start()
    {
        jsonData = JsonConvert.DeserializeObject<PointData>(jsonFile.ToString());   // JSON 파일 파싱

        width = jsonData.depth_data[0].Length;  // 이미지의 가로 크기
        height = jsonData.depth_data.Length;    // 이미지의 세로 크기
        
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    // getter/setter jsonFile
    public PointData GetJsonData()
    {
        return jsonData;
    }
    public void SetJsonFile(TextAsset jsonFile)
    {
        this.jsonFile = jsonFile;
    }

}
