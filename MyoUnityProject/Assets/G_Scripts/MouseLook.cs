using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    void Start()
    {
        //Cursor won't be seen in the game view
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //When you not using Myo
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        //Rotate the player with mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
