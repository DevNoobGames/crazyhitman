using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotatio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomVal = Random.Range(0, 359);
        transform.eulerAngles = new Vector3(0, randomVal, 0);
    }
}
