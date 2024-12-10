using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCameraToggle : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float sensitivity = 2f; // Mouse sensitivity

    private float xRotation = 0f; // Vertical rotation
    [SerializeField] private GameObject[] virtualCameras; // Array to hold all virtual cameras
    public int currentCameraIndex = 0; // Index of the currently active camera
    private bool isFreeFlyMode = false; // To track if in free-fly mode

    [SerializeField] private GameObject freeFlyCamera; // The camera used for free-fly mode

    void Start()
    {
        // Ensure only the first camera is active initially
        ActivateCamera(currentCameraIndex);
        if (freeFlyCamera != null)
        {
            freeFlyCamera.SetActive(false); // Start with the free-fly camera inactive
        }
    }

    void Update()
    {
        if (isFreeFlyMode && freeFlyCamera != null)
        {
            HandleFreeFlyControls();
        }
        else
        {
            // Check for input to toggle cameras (e.g., press "C")
            if (Input.GetKeyDown(KeyCode.C)) // Replace "C" with your desired key
            {
                ToggleNextCamera();
            }
        }
    }

    public void ToggleNextCamera()
    {
        if (isFreeFlyMode) return; // Prevent toggling in free-fly mode

        // Deactivate the current camera
        virtualCameras[currentCameraIndex].SetActive(false);

        // Increment the camera index and loop back if needed
        currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Length;

        // Activate the next camera
        virtualCameras[currentCameraIndex].SetActive(true);
    }

    public void ActivateCamera(int index)
    {
        // Activate only the specified camera, deactivate others
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].SetActive(i == index);
        }
    }

    public void EnableFreeFlyMode()
    {
        // Deactivate all fixed cameras and enable the free-fly camera
        foreach (GameObject cam in virtualCameras)
        {
            cam.SetActive(false);
        }
        if (freeFlyCamera != null)
        {
            freeFlyCamera.SetActive(true);
        }
        isFreeFlyMode = true;
    }

    public void DisableFreeFlyMode()
    {
        // Disable the free-fly camera and return to the fixed camera system
        if (freeFlyCamera != null)
        {
            freeFlyCamera.SetActive(false);
        }
        ActivateCamera(currentCameraIndex); // Reactivate the current fixed camera
        isFreeFlyMode = false;
    }

    private void HandleFreeFlyControls()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the camera horizontally (left and right) by rotating its parent object
        freeFlyCamera.transform.Rotate(Vector3.up * mouseX, Space.World);

        // Rotate the camera vertically (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Restrict vertical rotation
        freeFlyCamera.transform.localRotation = Quaternion.Euler(xRotation, freeFlyCamera.transform.localRotation.eulerAngles.y, 0f);

        // Get movement input
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys
        float ascendInput = Input.GetKey(KeyCode.Space) ? 1 : (Input.GetKey(KeyCode.LeftShift) ? -1 : 0); // Space for up, Shift for down

        // Create movement vector
        Vector3 movement = new Vector3(horizontalInput, ascendInput, verticalInput) * speed;

        // Transform the movement into world space
        movement = freeFlyCamera.transform.TransformDirection(movement);

        // Apply movement
        freeFlyCamera.transform.position += movement * Time.deltaTime;
    }

public void FocusOnPlanet(Transform planetTransform)
{
    // Deactivate all cameras
    foreach (GameObject cam in virtualCameras)
    {
        cam.SetActive(false);
    }

    // Ensure the current camera is activated and updated
    if (planetTransform != null)
    {
        GameObject activeCamera = virtualCameras[currentCameraIndex];

        // Position the camera dynamically relative to the selected planet
        Vector3 offset = planetTransform.forward * 15f + planetTransform.up * 5f; // Adjust as needed
        activeCamera.transform.position = planetTransform.position + offset;
        activeCamera.transform.LookAt(planetTransform.position);

        // Activate only the current camera
        activeCamera.SetActive(true);

        Debug.Log($"Camera successfully focused on {planetTransform.name}");
    }
    else
    {
        Debug.LogWarning("Invalid planet transform! No focus applied.");
    }
}
}