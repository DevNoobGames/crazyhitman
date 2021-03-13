using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public CharacterController Char;

    // Update is called once per frame
    void Update()
    {
        if (Char.isGrounded)
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Flying");
        }
    }
}
