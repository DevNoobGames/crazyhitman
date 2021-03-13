using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Arrow : MonoBehaviour
{
    public Vector3 startPos;
    public GameObject target;
    public TextMeshProUGUI distanceText;

    private void Start()
    {
        //startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
        float Distance = Vector3.Distance(transform.parent.transform.position, target.transform.position);
        distanceText.text = (Distance / 5).ToString("00") + "m";
        if (Distance > 25)
        {
            transform.localPosition = startPos;
        }
        else
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 3, target.transform.position.z);
        }
    }
}
