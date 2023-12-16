using UnityEngine;


public interface ICamUpdatable
{
    void UpdateRotation(GameObject target, float sensitivity);
    void UpdateCameraRotation(float sensitivity);
}

public class CameraController : MonoBehaviour, ICamUpdatable
{
    [SerializeField] private float camRotationLimit;      // 카메라 회전 제한
    private float currentCamRotationX; // 마우스 좌 / 우 이동으로 카메라 y축 회전
    private float currentCamRotationY; // 마우스 위 / 아래 이동으로 카메라 x축 회전

    /// <summary>
    /// 받은 오브젝트와 함께 회전
    /// </summary>
    /// <param name="target">회전시킬 대상</param>
    /// <param name="sensitivity">회전 속도, 감도</param>
    public void UpdateRotation(GameObject target, float sensitivity)
    {
        float mouseX = Input.GetAxisRaw("Mouse Y");
        float mouseY = Input.GetAxisRaw("Mouse X");

        float cameraRotationX = mouseX * sensitivity;
        float cameraRotationY = mouseY * sensitivity;

        // 카메라 회전
        currentCamRotationX -= cameraRotationX;
        currentCamRotationY += cameraRotationY;
        
        currentCamRotationX = ClampAngle(currentCamRotationX, -camRotationLimit, camRotationLimit);

        target.transform.localEulerAngles = new Vector3(currentCamRotationX, currentCamRotationY, 0f);   
    }

    /// <summary>
    /// 카메라만 회전
    /// </summary>
    /// <param name="sensitivity">회전 속도, 감도</param>
    public void UpdateCameraRotation(float sensitivity)
    {
        UpdateRotation(gameObject, sensitivity);
    }

    // 카메라 회전 범위 설정
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 0f)
        {
            angle += 360f;
        }
        angle %= 360;

        return Mathf.Clamp(angle, min, max);
    }

}
