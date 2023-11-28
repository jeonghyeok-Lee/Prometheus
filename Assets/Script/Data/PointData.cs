// 포인트 클라우드에서 사용할 Point데이터 클래스

[System.Serializable]
public class PointData
{
    public Location location;   // 자동차의 위치, 회전 정보
    public int[][] depth_frame;  // 포인트의 깊이 데이터
}
// 자동차의 x,y,z값 저장
[System.Serializable]
public class Location
{
    public float x;
    public float y;
    public float[] yaw;
}
