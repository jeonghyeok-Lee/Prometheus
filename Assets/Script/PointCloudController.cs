using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace testest{

    [System.Serializable]
    public class PointData
    {
        public float X;
        public float Y;
        public float Z;
    }

    [System.Serializable]
    public class PointCloudData
    {
        public PointData[] Points;
    }


    public class PointCloudController : MonoBehaviour
    {
        public TextAsset jsonFile; // JSON 파일을 할당하기 위한 변수
        public TextAsset jsonFile2; // JSON 파일을 할당하기 위한 변수[테스트용]

        public Material pointCloudMaterial; // 포인트 클라우드를 렌더링할 Material

        public float distanceRatio = 0.1f; // 포인트 클라우드의 거리 비율

        void Start()
        {
            Test();

            // DefaultTest();

        }

        private void DefaultTest(){
            // JSON 파일을 읽어와서 PointCloudData 객체로 파싱
            PointCloudData pointCloudData = JsonUtility.FromJson<PointCloudData>(jsonFile.text);

            // 포인트 클라우드를 생성하고 위치 정보를 설정
            GameObject pointCloudObject = new GameObject("PointCloud");
            pointCloudObject.transform.position = Vector3.zero;

            // 각 포인트에 대해 메시에 추가
            Mesh pointCloudMesh = new Mesh();
            Vector3[] vertices = new Vector3[pointCloudData.Points.Length];

            for (int i = 0; i < pointCloudData.Points.Length; i++)
            {
                vertices[i] = new Vector3(pointCloudData.Points[i].X, pointCloudData.Points[i].Y, pointCloudData.Points[i].Z);
            }

            pointCloudMesh.vertices = vertices;
            pointCloudMesh.SetIndices(Enumerable.Range(0, pointCloudData.Points.Length).ToArray(), MeshTopology.Points, 0);

            // 메시 필터 및 렌더러 추가
            MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

            meshFilter.mesh = pointCloudMesh;
            meshRenderer.material = pointCloudMaterial;
        }

        // 재작성 필요 현재 data의 값을 가져오지 못하고 있음..
        private void Test(){

            // 테스트용
            // PointDataTest json = JsonUtility.FromJson<PointDataTest>(jsonFile2.text);
            PointDataTest jsonTest = JsonConvert.DeserializeObject<PointDataTest>(jsonFile2.ToString());             // 스캔한 데이터
            Debug.Log(jsonTest.depth_data.Length);
            Debug.Log(jsonTest.depth_data[0].Length);
            
            int number = 0;

            // 좌표로 변환
            List<Vector3> unityCoordinates = new List<Vector3>();

            // 포인트 클라우드를 생성하고 위치 정보를 설정
            GameObject pointCloudObject = new GameObject("PointCloud");
            pointCloudObject.transform.position = Vector3.zero;

            // 각 포인트에 대해 메시에 추가
            Mesh pointCloudMesh = new Mesh();
            Vector3[] vertices = new Vector3[jsonTest.depth_data.Length];

            // 데이터가 배열의 배열이므로 두 번의 루프를 사용
            for (int i = 0; i < jsonTest.depth_data.Length; i++)
            {
                int[] row = jsonTest.depth_data[i];

                for (int j = 0; j < row.Length; j++)
                {
                    float depthValue = row[j];  // 깊이 데이터
                
                    // 깊이를 통해 포인터들의 좌표를 계산
                    Vector3 coordinate = CalPointPosAndRot(0, depthValue, pointCloudObject, number++);

                    vertices[i] = coordinate;   // 메시에 추가

                    // 계산된 좌표를 리스트에 추가
                    unityCoordinates.Add(coordinate);

                }
            }

            pointCloudMesh.vertices = vertices;
            pointCloudMesh.SetIndices(Enumerable.Range(0, jsonTest.depth_data.Length).ToArray(), MeshTopology.Points, 0);

            // 메시 필터 및 렌더러 추가
            MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

            meshFilter.mesh = pointCloudMesh;
            meshRenderer.material = pointCloudMaterial;


            // int count = 0;
            // // 변환된 좌표 출력
            // foreach (var coordinate in unityCoordinates)
            // {
            //     Debug.Log("좌표 : " + coordinate + " / " + count++ + "번째" + " / " + zeroCount);
            // }

        }

        /// <summary>
        /// 포인트의 위치 및 회전값을 계산하는 메서드
        /// </summary>
        /// <param name="angle">중심점과의 각도 포인터가 위치할 각도</param>
        /// <param name="distance">중심점과의 거리 포인터가 중심점으로부터 떨어진 거리</param>
        private Vector3 CalPointPosAndRot(float angle, float distance, GameObject middle, int number)
        {
            // 중심점[플레이어]의 회전값을 변경
            middle.transform.rotation = Quaternion.Euler(middle.transform.rotation.x, angle, middle.transform.rotation.z);

            // 포인트컨트롤러가 바라보는 방향에서 0.01f만큼 떨어진 위치를 계산
            Vector3 directionFromCenter = middle.transform.forward * distance * distanceRatio;

            // number%320만큼 x좌표 값을 증가
            directionFromCenter.x += (number % 320) * distanceRatio;
            if(number / 320 > 0){
                directionFromCenter.y += (number / 320) * distanceRatio;
            }

            // 포인트가 생성될 위치를 계산
            Vector3 spanwPos = middle.transform.position + directionFromCenter;

            return spanwPos;
        }





        /// <summary>
        /// //////////////////////////////// 테스트용
        /// </summary>
        [System.Serializable]
        public class PointDataTest
        {
            public Location location;
            public int[][] depth_data;
        }

        [System.Serializable]
        public class Location
        {
            public float x;
            public float y;
            public float z;
        }

    }
}
