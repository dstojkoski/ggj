using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CameraController : MonoBehaviour
{
    // Movement
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.7f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;

    // Camera
    public Camera playerCamera;
    public float lookSpeed = 2.0f;     // mouseSensitivity;
    public float lookXLimit = 70.0f;
    private float rotationX = 0;

    private CharacterController characterController;

    
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
        HandleMovement();
        HandleCamera();
    }

    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;

        }

        controller.Move(velocity * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);

    }

    void HandleCamera()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
