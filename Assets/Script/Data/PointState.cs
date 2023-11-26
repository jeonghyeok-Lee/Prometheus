/// <summary>
/// 포인트 상태 정보를 가지고 있는 클래스
/// </summary>
public class PointState
{
    public float LimitDepth { get; set; }			// 포인트 클라우드의 깊이 제한
    public float DistanceFromCar { get; set; }	
    public int Size { get; set; }
	public float PointRatio { get; set; }

	/// <summary>
	/// 포인트 상태 정보를 가지고 있는 클래스 생성자
	/// </summary>
	/// <param name="limitDepth">깊이 제한</param>
	/// <param name="distanceFromCar">RC카와 출력하려는 포인트들의 거리</param>
	/// <param name="size">원할한 출력을 위한 이미지 분할 횟수</param>
	/// <param name="pointRatio">이미지 크기 비율</param>
    public PointState(float limitDepth, float distanceFromCar, int size, float pointRatio)
	{
		LimitDepth = limitDepth;
		DistanceFromCar = distanceFromCar;
		Size = size;
		PointRatio = pointRatio;
    }
}