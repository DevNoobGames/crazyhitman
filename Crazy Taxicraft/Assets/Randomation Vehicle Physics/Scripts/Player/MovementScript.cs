using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float drivingSpeed = 7.5f;
    public float rotateSpeed = 1;
    public float gravity = 20.0f;

    [HideInInspector]
    public bool canMove = true;
    Vector3 moveDirection = Vector3.zero;
    //float rotationX = 0;

    public CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? (drivingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (drivingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        //moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection = (forward * curSpeedX);
        moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
        {
            moveDirection += Physics.gravity;
            //moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);

        /*if (Input.GetKey(KeyCode.W) && controller.isGrounded)
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
        }
        if (Input.GetKey(KeyCode.S) && controller.isGrounded)
        {
            rb.AddRelativeForce(Vector3.back * thrust);
        }*/

        if (Input.GetKey(KeyCode.A) && characterController.isGrounded)
        {
            transform.Rotate(0, -rotateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) && characterController.isGrounded)
        {
            transform.Rotate(0, rotateSpeed, 0);
        }

    }
}
