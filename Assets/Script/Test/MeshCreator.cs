using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        vertices = new Vector3[]{
            new Vector3(0,0,0), new Vector3(0.5f,1,0), new Vector3(1,0,0)
        };

       triangles = new int[]{
            0,1,2
        };

        // メッシュを作成する
        OnPolygonMeshCreate(vertices, triangles);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            for(int i = 0; i < vertices.Length; i++){
                Debug.Log(vertices[i]);
                vertices[i] = new Vector3(vertices[i].x+1, vertices[i].y , vertices[i].z);
                Debug.Log(vertices[i]);
            }
            OnPolygonMeshCreate(vertices, triangles);
        }
    }

    // メッシュを作成する
    private void OnPolygonMeshCreate(Vector3[] vertices, int[] triangles){
        Mesh mesh = new Mesh();                 // 메쉬 생성
        mesh.vertices = vertices;               // 메쉬 버텍스 설정
        mesh.triangles = triangles;             // 메쉬 트라이앵글 설정
        GetComponent<MeshFilter>().mesh = mesh; // 메쉬 필터에 메쉬 적용
    }
}
