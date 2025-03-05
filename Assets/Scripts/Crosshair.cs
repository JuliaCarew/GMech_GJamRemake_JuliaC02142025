using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false; // Hide the default cursor
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Ensure it's in front of the camera
        transform.position = mainCamera.ScreenToWorldPoint(mousePos);
    }
}
