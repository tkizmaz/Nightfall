using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    
    public float sensitivity = 10f;
    public float minPitch = -30f;
    public float maxPitch = 60f;
    public Transform parent;
    public Transform boneParent;
    private float pitch = 0f;
    [HideInInspector] public float yaw = 0f;
    [HideInInspector] public float relativeYaw = 0f;
    public float xRotation = 0f;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        CameraRotate();
        transform.position = boneParent.position;
        boneParent.rotation = transform.rotation;
    }

    void CameraRotate()
    {
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;    
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        parent.Rotate(Vector3.up * mouseX);
}
}
