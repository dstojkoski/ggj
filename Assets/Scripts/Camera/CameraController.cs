using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

public class CameraController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    // mouseSensitivity;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    
    [HideInInspector]
    public bool canMove = true;
    //public float mouseSensitivity = 100f;
    //public float moveSpeed = 100f;
    
    /*
    private float xRotation = 0f;
    private float yRotation = 0f;  // Variable for horizontal movement limit
    public float maxYRotation = 80f;  // Limit for the horizontal rotation
    public float minYRotation = -80f; // Limit for the horizontal rotation
    */

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
       Vector3 forward = transform.TransformDirection(Vector3.forward);
       Vector3 right = transform.TransformDirection(Vector3.right);
       
       // Left Shift to run
       bool isRunning = Input.GetKey(KeyCode.LeftShift);
       float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
       float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
       float movementDirectionY = moveDirection.y;
       moveDirection = (forward * curSpeedX) + (right * curSpeedY);

       if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
       {
           moveDirection.y = jumpSpeed;
       }
       else
       {
           moveDirection.y = movementDirectionY;
       }
       
       // Apply gravity. Multiply gravity by deltatime twice (once here 
       // and once belowe when moveDirection is multiplied by deltaTime)
       // This is done because gravity is applied as an acceleration (ms^-2)
       if (!characterController.isGrounded)
       {
           moveDirection.y -= gravity * Time.deltaTime;
       }
       
       // Move the controller
       characterController.Move(moveDirection * Time.deltaTime);


       if (canMove)
       {
           rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
           rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
           playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
           transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
       } 

        /*
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
        */
    }
}
