using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataController
{
    private TextAsset jsonFile; //  JSON 파일을 로드하기 위한 변수

    private PointData jsonData; //  JSON 파일을 파싱하여 할당하기 위한 변수

    private string filePath;    //  JSON 파일의 경로
    
    private int width;      //  이미지의 가로 크기
    private int height;     //  이미지의 세로 크기

    public DataController()
    {
        filePath = "json/data3/";
        SetJsonData("depth_data_1");
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public string FilePath{
        get { return filePath; }
        set { filePath = value; }
    }

    // getter/setter jsonFile
    public PointData GetJsonData()
    {
        return jsonData;
    }

    /// <summary>
    /// JSON 파일을 파싱하여 jsonData에 할당 및 가로, 세로 크기 할당
    /// </summary>
    /// <param name="fileName">로드하고자 하는 jsonFile의 이름</param>
    public void SetJsonData(string fileName){
        jsonFile = Resources.Load<TextAsset>(filePath + fileName);
        
        if(jsonFile != null){
            jsonData = JsonConvert.DeserializeObject<PointData>(jsonFile.ToString());
            width = jsonData.depth_frame[0].Length;
            height = jsonData.depth_frame.Length;
        }
        else{
            Debug.LogError("JSON 파일 로드에 실패하였습니다. 경로: "+ filePath + fileName);
        }

    }

    /// <summary>
    /// filePath 경로에 존재하는 파일들의 이름을 반환
    /// </summary>
    /// <returns>string[]타입의 파일 이름 반환</returns>
    public string[] GetFileList(){
        string[] files = System.IO.Directory.GetFiles("Assets/Resources/" + filePath);
        return files;
    }
    
    /// <summary>
    /// filePath 경로에 존재하는 파일들의 개수를 반환
    /// </summary>
    /// <returns>파일 개수 반환</returns>
    public int GetFileCount(){
        int fileCount = GetFileList().Length;

        // .meta 파일 제외
        return fileCount/2;
    }


}
