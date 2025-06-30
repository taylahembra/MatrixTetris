using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// Events
// Handles events, currently just death

public class Events : MonoBehaviour
{
    public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        OnDeath = new UnityEvent();
        OnDeath.AddListener(ChangeCameras);
        Camera uiDeath = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Camera>();
        uiDeath.enabled = false;
    }

    public void OnDeathTriggered()
    {
        OnDeath.Invoke();
    }

    public void ChangeCameras()
    {
        // Get cameras for ui
        Camera ui = GameObject.FindGameObjectWithTag("UI").GetComponent<Camera>();
        Camera uiDeath = GameObject.FindGameObjectWithTag("UIDeath").GetComponent<Camera>();

        // Make player unable to move
        CameraMovement camera = Camera.main.GetComponent<CameraMovement>();
        camera.allowMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // set UIs
        if (!ui.IsUnityNull()) ui.enabled = false;
        if (!uiDeath.IsUnityNull()) uiDeath.enabled = true;
    }
}
