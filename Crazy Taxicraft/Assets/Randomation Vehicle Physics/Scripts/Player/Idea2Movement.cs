using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idea2Movement : MonoBehaviour
{
    public float drivingSpeed = 7.5f;
    public float rotateSpeed = 1;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    [HideInInspector]
    public bool canMove = true;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (drivingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (drivingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

            //moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            moveDirection = (forward * curSpeedX);


        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
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

        if (canMove && Input.GetAxis("Vertical") > 0)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
