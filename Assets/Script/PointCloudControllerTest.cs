using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public class PointCloudControllerTest : MonoBehaviour
    {

        public TextAsset jsonFile; // JSON 파일을 할당하기 위한 변수
        public Material pointCloudMaterial; // 포인트 클라우드를 렌더링할 Material

        // Start is called before the first frame update
        void Start()
        {
            DefaultTest();
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

    }


}
