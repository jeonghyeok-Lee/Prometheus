using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Transform carTransform;  // RCCar의 Transform
    private Vector3 carPosition;    // 위치
    private Vector3 carForward;     // 방향
    private Quaternion carRotation; // 회전

    // Start is called before the first frame update
    void Start()
    {
        carTransform = GetComponent<Transform>();   // RCCar의 Transform을 가져옴
        carPosition = carTransform.position;
        carRotation = carTransform.rotation;
        carForward = carTransform.forward;
        
    }

    // Update is called once per frame
    void Update()
    {
        carPosition = carTransform.position;
        carRotation = carTransform.rotation;
        carForward = carTransform.forward;
    }

    // getter/setter
    public Vector3 CarPosition
    {
        get { return carPosition; }
        set { carPosition = value; }
    }

    public Quaternion CarRotation
    {
        get { return carRotation; }
        set { carRotation = value; }
    }

    public Vector3 CarForward
    {
        get { return carForward; }
        set { carForward = value; }
    }
}
