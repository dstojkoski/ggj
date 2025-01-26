using System;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.06f;
    public float maxSwayAmount = 0.12f;
    public float smoothAmount = 6f;
    public float bobFrequency = 4f;
    public float bobAmplitude = 0.035f;
    private float bobTimer = 0f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float swayOffset = 0.0f;
        // Check if there's movement
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            // Calculate sway offset using a sine wave
            swayOffset = Mathf.Sin(bobTimer) * bobAmplitude;

            // Apply the sway offset to the gun's local position
            // Vector3 newPosition =  transform.localPosition;
            // newPosition.x = swayOffset; // Sway left and right
            // transform.localPosition = newPosition;
        }
        else
        {
            // Reset the sway when there's no movement
            bobTimer = 0f;
            // Vector3 resetPosition = transform.localPosition;
            // resetPosition.x = 0f;
            // transform.localPosition = resetPosition;
            swayOffset = 0.0f;
        } 
        
        
        moveX = Mathf.Clamp(moveX + swayOffset, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);
        
        Vector3 targetPosition = new Vector3(moveX, moveY, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
