using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private bool movable;

    private Vector3 previousPosition;

    private void Start()
    {
        movable = true;
    }
    void Update()
    {
        if (movable) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

                //camera.transform.position = new Vector3();

                camera.transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), -direction.x * 180);

                previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
            }
        }
        else
        {
            camera.transform.position = new Vector3(5, 5, -5);
            camera.transform.localEulerAngles = new Vector3(30, -45, 0);
        }
    }

    public void setMovable(bool isMovable)
    {
        movable = isMovable;
    }
}
