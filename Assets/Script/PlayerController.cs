using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody playerRigidbody; // 케릭터 리지드바디

    private RotateToMouse rotateToMouse;                                            // 마우스 이동으로 카메라 회전

    private void Awake()
    {
        rotateToMouse = GetComponent<RotateToMouse>();

        playerRigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();

        // 마우스 좌클릭하며 마우스 이동시 카메라 화면 전환
        if (Input.GetMouseButton(0)){
            CameraRotation();
        }
    }

    private void Move(){
        // 케릭터 이동
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;
        
        playerRigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void CameraRotation(){
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }
}
