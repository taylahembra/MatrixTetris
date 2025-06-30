using UnityEngine;

// Camera
// Moves the camera to the player's position

public class CameraScript : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
