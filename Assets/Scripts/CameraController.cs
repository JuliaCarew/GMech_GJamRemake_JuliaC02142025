using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 2D camera follows the player and makes the player always in the center of the screen

    public Transform player; 
    public float smoothSpeed = 5f; // Smoothing speed for the camera movement
    public float zoomSpeed = 5f; // Speed of zooming
    public float minZoom = 5f; // Minimum zoom distance
    public float maxZoom = 15f; // Maximum zoom distance

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (player == null)
        {
            Debug.LogError("Player not assigned to the Camera Controller!");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Smoothly follow the player
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Handle camera zoom with mouse scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scrollInput * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}