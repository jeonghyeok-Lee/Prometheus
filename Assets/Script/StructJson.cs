using UnityEngine;

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
        public ScanData[] data;
    }

    public class LoadJson
    {
        private ScanDataArray data;

        public ScanDataArray Data
        {
            get { return data; }
        }

        /// <summary>
        /// 경로에 있는 json 파일을 읽는 생성자
        /// </summary>
        /// <param name="path">json 파일이 있는 상대 경로</param>
        public LoadJson(string path)
        {
            TextAsset loadedJson = Resources.Load<TextAsset>(path);
            data = JsonUtility.FromJson<ScanDataArray>(loadedJson.ToString());
        }

    }
}

