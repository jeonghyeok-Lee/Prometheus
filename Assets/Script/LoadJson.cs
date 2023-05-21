using UnityEngine;
using Newtonsoft.Json;
using System;

namespace JsonSetting
{
    /// <summary>
    /// Json 데이터
    /// </summary>
    [System.Serializable]
    public struct ScanData
    {
        public string angle;
        public string distance;
    }

    [System.Serializable]
    public struct ScanDataArray
    {
        public ScanData[][] data;
    }

    public class LoadJson
    {
        private ScanDataArray scanData;

        public ScanDataArray ScanData
        {
            get { return scanData; }
        }

        /// <summary>
        /// 경로에 있는 json 파일을 읽는 생성자
        /// </summary>
        /// <param name="path">json 파일이 있는 상대 경로</param>
        public LoadJson(string path)
        {
            setPath(path);
        }

        public void setPath(string path)
        {
            try
            {
                TextAsset loadedJson = Resources.Load<TextAsset>(path);
                scanData = JsonConvert.DeserializeObject<ScanDataArray>(loadedJson.ToString());         
            }catch (Exception e)
            {
                Debug.LogError(e);
                Debug.LogError("Failed to load JSON file at path: " + path);
            }
        }

    }
}

