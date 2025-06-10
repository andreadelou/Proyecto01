using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;
    private CharacterController characterController;
    public float cameraRotationSpeed = 200f;
    public Transform cameraFollow;
    private float pitch = 0f;
    public Transform model;
    private Animator animator;
    private float verticalVelocity = 0f;
    public float jumpForce = 0.2f;
    public float gravity = -10f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movementVector = cameraFollow.right * moveX + cameraFollow.forward * moveZ;
        movementVector.y = 0;
        movementVector.Normalize();


        // Gravedad y salto
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;

            if (Input.GetKeyDown(KeyCode.LeftShift)) // Salto con Shift izquierdo
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = movementVector * movementSpeed;
        finalMove.y = verticalVelocity;

        characterController.Move(finalMove * Time.deltaTime);

        if (movementVector.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(movementVector);
            model.rotation = rotation;
        }

        animator.SetFloat("Speed", movementVector.magnitude);

        void CameraMovement()
        {

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            mouseX = mouseX * cameraRotationSpeed * Time.deltaTime;
            mouseY = mouseY * cameraRotationSpeed * Time.deltaTime;

            cameraFollow.Rotate(Vector3.up * mouseX);

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -20f, 40f);

            cameraFollow.localRotation = Quaternion.Euler(pitch, cameraFollow.localEulerAngles.y, 0);
        }

    }

    
}