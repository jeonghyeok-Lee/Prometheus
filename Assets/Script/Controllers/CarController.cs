using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Transform carTransform;  // RCCar의 Transform
    private Vector3 carPosition;    // 위치
    private Vector3 carForward;     // 방향
    private Quaternion carRotation; // 회전

    public CarController(Transform carTransform)
    {
        this.carTransform = carTransform;
        carPosition = carTransform.position;
        carForward = carTransform.forward;
        carRotation = carTransform.rotation;
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
