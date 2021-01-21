using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{

    public float SensitivityX = 15f;

    public float SensitivityY = 15f;

    public float OrbitDistance = 1f;

    public Vector2 InitialRotation;

    public Transform OrbitCenter;

    private float rotationX;
    private float rotationY;

    private Vector3 lastMousePosition;

    private void Awake()
    {
        rotationX = InitialRotation.x;
        rotationY = InitialRotation.y;
    }

    void Update()
    {
        Vector2 delta = Vector2.zero;

        if (lastMousePosition == Vector3.zero)
            lastMousePosition = Input.mousePosition;

        if (Input.touchCount == 1)
            delta = Input.GetTouch(0).deltaPosition;
        else if (Input.GetMouseButton(0))
        {
            delta.x = Input.GetAxis("Mouse X");
            delta.y = Input.GetAxis("Mouse Y");
        }

        rotationX += delta.x * SensitivityX * Time.deltaTime;
        rotationY += delta.y * SensitivityY * Time.deltaTime;

        var lookRotation = Quaternion.Euler(rotationY, rotationX, 0);

        transform.position = OrbitCenter.position + lookRotation * (Vector3.back * OrbitDistance);
        transform.rotation = lookRotation;

        lastMousePosition = Input.mousePosition;
    }
}
