using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[RequireComponent(typeof(CharacterController))]


public class FPScontroller : MonoBehaviour
{
    public GameObject player;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchingSpeed = 3.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float crouchingHeight = 1.0f;
    public float standingHeight = 2.0f;

    public Slider staminaBar; // UI Slider for stamina
    public float maxStamina = 5f; 
    public float staminaDrainRate = 1f; 
    public float staminaRegenRate = 0.5f;
    private float currentStamina;
    public bool isSprintingAllowed = true; // Prevents sprinting if stamina is empty

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private bool isCrouching = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.height = standingHeight;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);

        // Initialize Stamina
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isSprintingAllowed;
        
        // Handle stamina
        if (isRunning && canMove && !isCrouching)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isSprintingAllowed = false; // Disable sprinting when stamina is empty
            }
        }
        else
        {
            // Regenerate stamina when not sprinting
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            if (currentStamina >= 1f) // Allow sprinting again if stamina is enough
                isSprintingAllowed = true;
        }

        staminaBar.value = currentStamina; // Update UI

        // Movement logic
        float curSpeedX = canMove ? (isCrouching ? crouchingSpeed : (isRunning ? runningSpeed : walkingSpeed)) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isCrouching ? crouchingSpeed : (isRunning ? runningSpeed : walkingSpeed)) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isCrouching)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Crouch logic
        if (Input.GetKeyDown(KeyCode.C) && player.activeSelf == true)
        {
            isCrouching = !isCrouching;
            characterController.height = isCrouching ? crouchingHeight : standingHeight;

            Vector3 position = transform.position;
            position.y += isCrouching ? -0.5f * (standingHeight - crouchingHeight) : 0.5f * (standingHeight - crouchingHeight);
            transform.position = position;
        }
    }
}
