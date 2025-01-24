using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float moveSpeed = 100f;

    private float xRotation = 0f;
    private float yRotation = 0f;  // Variable for horizontal movement limit
    public float maxYRotation = 80f;  // Limit for the horizontal rotation
    public float minYRotation = -80f; // Limit for the horizontal rotation

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical movement limitation (already in place)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Horizontal movement limitation
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minYRotation, maxYRotation);

        // Applying the rotations
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Movement (forward/backward, left/right)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
