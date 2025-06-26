using System;
using UnityEngine;

// Movement
// This script controls the player's mouse movement. It does not control keyboard movement

public class CameraMovement : MonoBehaviour
{
    public bool allowMove = false;
    public float sensX, sensY;
    public float reach;
    [HideInInspector] public Vector3 touch;

    public Transform orientation;
    float xRotation, yRotation;
    // Start is called before the first frame update
    void Start()
    {
        allowMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        // Allow enter to switch between UI and movement
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (allowMove)
            {
                // Go to UI
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                allowMove = false;
            }
            else if (!allowMove)
            {
                // Go to main screen
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                allowMove = true;
            }
        }

        if (allowMove)
        {
            // Get where the pointer is
            touch = transform.position + reach * transform.forward;

            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            // Rotate camera
            yRotation += mouseX;
            xRotation -= mouseY;

            // Keep rotation to 180 degree
            xRotation = Math.Clamp(xRotation, -90f, 90f);

            // Rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);  // Camera
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);        // Player but only y
        }

    }
}
