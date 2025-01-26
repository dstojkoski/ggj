using UnityEngine;
using UnityEngine.Serialization;

public class Sway2 : MonoBehaviour
{
    [Header("Position Sway")]
    public float movementSwayFrequency = 2f; 
    public float movementSwayAmplitude = 0.1f; 

    [Header("Rotation Sway")]
    public float rotationSwaySensitivity = 2f; 
    public float maxRotationSway = 5f; 

    private float movementSwayTimer = 0f; 

    // [FormerlySerializedAs("weaponTransform")] [Header("References")]
    // public Transform transform; 

    void Update()
    {
        HandleMovementSway();
        HandleRotationSway();
    }

    private void HandleMovementSway()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            movementSwayTimer += Time.deltaTime * movementSwayFrequency;

            float swayOffset = Mathf.Sin(movementSwayTimer) * movementSwayAmplitude;

            Vector3 newPosition = transform.localPosition;
            newPosition.x = swayOffset;
            transform.localPosition = newPosition;
        }
        else
        {
            movementSwayTimer = 0f;
            Vector3 resetPosition = transform.localPosition;
            resetPosition.x = 0f;
            transform.localPosition = resetPosition;
        }
    }

    private void HandleRotationSway()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float swayX = -mouseX * rotationSwaySensitivity;
        float swayY = mouseY * rotationSwaySensitivity;

        swayX = Mathf.Clamp(swayX, -maxRotationSway, maxRotationSway);
        swayY = Mathf.Clamp(swayY, -maxRotationSway, maxRotationSway);

        Quaternion swayRotation = Quaternion.Euler(swayY, swayX, 0);
        transform.localRotation = swayRotation;
    }
}
