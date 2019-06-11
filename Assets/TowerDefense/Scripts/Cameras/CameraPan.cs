using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Camera attachedCamera;
    public float movementThreshold = .25f; // Percentage offset where movement starts (25%)
    public float movementSpeed = 20f;
    public float zoomSensitivity = 10f;
    public float resetSpeed = 10f;

    public bool useMouse = false;
    public bool resetToCenter = false;

    public Vector3 size = new Vector3(20f, 1f, 20f);
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    /// <summary>
    /// Filters incoming position to keep it within bounds
    /// </summary>
    /// <param name="incomingPos">Position that needs filtering</param>
    /// <returns></returns>
    Vector3 GetAdjustedPos(Vector3 incomingPos)
    {
        // Storing in smaller name
        Vector3 pos = transform.position;

        // Getting half size
        Vector3 halfSize = size * 0.5f;

        // X
        if (incomingPos.x > pos.x + halfSize.x)
            incomingPos.x = pos.x + halfSize.x;

        if (incomingPos.x < pos.x - halfSize.x)
            incomingPos.x = pos.x - halfSize.x;

        // Y
        if (incomingPos.y > pos.y + halfSize.y) incomingPos.y = pos.y + halfSize.y;
        if (incomingPos.y < pos.y - halfSize.y) incomingPos.y = pos.y - halfSize.y;

        // Z
        if (incomingPos.z > pos.z + halfSize.z) incomingPos.z = pos.z + halfSize.z;
        if (incomingPos.z < pos.z - halfSize.z) incomingPos.z = pos.z - halfSize.z;

        return incomingPos;
    }

    void Movement()
    {
        // Create transform for smaller name
        Transform camTransform = attachedCamera.transform;

        Vector3 input = Vector3.zero; // The direction to move the camera

        if (useMouse)
        {
            // Get mouse to viewport coorindates
            Vector2 mousePoint = attachedCamera.ScreenToViewportPoint(Input.mousePosition);
            // Calculate offset grom centre of screen
            Vector2 offset = mousePoint - new Vector2(.5f, .5f);
            // Get input only if offset reaches certain threshold
            if (offset.magnitude > movementThreshold)
                input = new Vector3(offset.x, 0, offset.y) * movementSpeed;
        }
        else
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            input = new Vector3(inputH, 0, inputV) * movementSpeed;
        }

        // Get scroll from axis and multiply the zoomSensitivity
        float inputScroll = Input.GetAxisRaw("Mouse ScrollWheel");
        Vector3 scroll = camTransform.forward * inputScroll * zoomSensitivity;

        // Apply movement
        Vector3 movement = input + scroll;
        camTransform.position += movement * Time.deltaTime;

        // Filter position with bounds
        camTransform.position = GetAdjustedPos(camTransform.position);

        if (input.magnitude < 1f && resetToCenter)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, transform.position, resetSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}