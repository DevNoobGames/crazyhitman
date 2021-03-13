using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    Vector3 moveDirection = Vector3.zero;
    public bool canMove = true;

    float curDir = 0f; // compass indicating direction
    float vertSpeed = 0f; // vertical speed (see note)
    Vector3 curNormal = Vector3.up; // smoothed terrain normal

    public bool OldSchool;
    public float turn;

    void Update()
    {
        if (OldSchool)
        {
            groundedPlayer = controller.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeedX = canMove ? (playerSpeed) * Input.GetAxis("Vertical") : 0;
            moveDirection = (forward * curSpeedX);
            controller.Move(moveDirection * Time.deltaTime);

            if (forward != Vector3.zero)
            {
                gameObject.transform.forward = forward;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (Input.GetKey(KeyCode.A) && Input.GetAxis("Vertical") > 0)
            {
                transform.Rotate(0, -rotateSpeed, 0);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetAxis("Vertical") > 0)
            {
                transform.Rotate(0, rotateSpeed, 0);
            }
            /*
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.5f, -transform.up, out hit, 5))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * 5.0f);
            }*/
            /*turn = Input.GetAxis("Horizontal") * rotateSpeed * 200 * Time.deltaTime;
            curDir = (curDir + turn) % 360; // rotate angle modulo 360 according to input
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, -transform.up, out hit, 5))
            {
                curNormal = Vector3.Lerp(curNormal, hit.normal, 4 * Time.deltaTime);
                Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
                transform.rotation = grndTilt * Quaternion.Euler(0, curDir, 0);
            }*/
        }


        if (!OldSchool)
        {
            float turn = Input.GetAxis("Horizontal") * rotateSpeed * 200 * Time.deltaTime;
            curDir = (curDir + turn) % 360; // rotate angle modulo 360 according to input
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -curNormal, out hit))
            {
                curNormal = Vector3.Lerp(curNormal, hit.normal, 4 * Time.deltaTime);
                Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
                transform.rotation = grndTilt * Quaternion.Euler(0, curDir, 0);
            }
            Vector3 movDir;
            movDir = transform.forward * Input.GetAxis("Vertical") * playerSpeed;
            // moves the character in horizontal direction (gravity changed!)
            if (controller.isGrounded) vertSpeed = 0; // zero v speed when grounded
            vertSpeed -= 9.8f * Time.deltaTime; // apply gravity
            movDir.y = vertSpeed; // keep the current vert speed
            controller.Move(movDir * Time.deltaTime); 
        }

    }
}