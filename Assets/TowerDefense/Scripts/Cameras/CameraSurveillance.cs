using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour
{
    public Camera[] cameras;            // Array of cameras to switch between
    public KeyCode prevKey = KeyCode.Q; // KeyCode to switch to previous camera
    public KeyCode nextKey = KeyCode.E; // KeyCode to switch to previous camera

    private int camIndex;               // Index into array of lookObjects
    private Camera current;             // Current target look object

    void Start()
    {
        // Get all camera children and store into array
        cameras = GetComponentsInChildren<Camera>();
        // Activate the default camera
        ActivateCamera(camIndex);
    }

    void Update()
    {
        // If the next key is pressed
        if (Input.GetKeyDown(nextKey))
        {
            // Increment index
            camIndex++;
            // If camIndex exceeds array size
            if (camIndex >= cameras.Length)
            {
                // Reset camIndex back to zero
                camIndex = 0;
            }
            // Activate camera
            ActivateCamera(camIndex);
        }

        // If the prev key is pressed
        if (Input.GetKeyDown(prevKey))
        {
            // Decrement index
            camIndex--;
            // If camIndex is below zero
            if (camIndex < 0)
            {
                // Set cam to last one in array
                camIndex = cameras.Length - 1;
            }
            // Activate camera
            ActivateCamera(camIndex);
        }
    }

    void ActivateCamera(int camIndex)
    {
        // Loop through all surveillance cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];
            // If the current index matches the argument camIndex
            if (i == camIndex)
            {
                // Enable this camera
                cam.gameObject.SetActive(true);
            }
            else // ...otherwise
            {
                // Disable this camera
                cam.gameObject.SetActive(false);
            }
        }
    }

}