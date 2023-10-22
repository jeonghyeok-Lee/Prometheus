using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace renderT{

    [System.Serializable]
    public class Point
    {
        public float X;
        public float Y;
        public float Z;
    }

    [System.Serializable]
    public class PointCloudData
    {
        public Point[] Points;
    }

    public class Render : MonoBehaviour
    {
        public GameObject pointPrefab; // 미리 만들어진 포인트 프리팹    
        public string jsonFileName = "json/TestData/output"; // .json 파일명 (확장자 제외)
        public int groupSize = 1000; // 그룹 크기

        void Start()
        {
            // .json 파일 로드
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

            // JSON 데이터 파싱
            PointCloudData pointCloudData = JsonUtility.FromJson<PointCloudData>(jsonFile.text);

            // 포인트 클라우드 시각화
            CreatePointCloudGroups(pointCloudData.Points);
        }

        void CreatePointCloudGroups(Point[] points)
        {
            int groupIndex = 0;
            GameObject currentGroup = null;

            foreach (var pointData in points)
            {
                if (groupIndex % groupSize == 0)
                {
                    // 새로운 그룹 생성
                    currentGroup = new GameObject("Group" + (groupIndex / groupSize));
                }

                Vector3 position = new Vector3(pointData.X, pointData.Y, pointData.Z);
                Instantiate(pointPrefab, position, Quaternion.identity, currentGroup.transform);

                groupIndex++;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
