using UnityEngine;
using Newtonsoft.Json;
using System;

namespace DataSetting
{
    namespace JsonSetting{
        public class DataJson
        {
            private ScanDataArray scanData;
            private TextAsset loadedJson = null;

            public ScanDataArray ScanData
            {
                get { return scanData; }
            }

            /// <summary>
            /// 경로에 있는 json 파일을 읽는 생성자
            /// </summary>
            /// <param name="path">json 파일이 있는 상대 경로</param>
            public DataJson(string path)
            {
                setPath(path);
            }


            /// <summary>
            /// JSON 경로 설정
            /// </summary>
            /// <param name="path">JSON데이터 경로 설정</param>
            public void setPath(string path)
            {
                try
                {
                    loadedJson = Resources.Load<TextAsset>(path);                                               // 해당 경로에 있는 JSON 데이터 로드
                    scanData = JsonConvert.DeserializeObject<ScanDataArray>(loadedJson.ToString());             // 스캔한 데이터
                }catch (Exception e)
                {
                    Debug.LogError(e);
                    Debug.LogError("Failed to load JSON file at path: " + path);
                }
                finally
                {
                    UnloadData(loadedJson);
                }
            }

            /// <summary>
            ///  로드한 리소스를 해제
            /// </summary>
            /// <param name="data">로드한 텍스트 파일</param>
            public void UnloadData(TextAsset data)
            {
                if (data != null) Resources.UnloadAsset(data);
            }

        }
    }

}

