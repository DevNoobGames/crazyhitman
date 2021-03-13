using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFol : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;


    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }


}
