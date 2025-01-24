using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float moveSpeed = 100f;

    private float xRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y + mouseX, 0f);

        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");  

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
