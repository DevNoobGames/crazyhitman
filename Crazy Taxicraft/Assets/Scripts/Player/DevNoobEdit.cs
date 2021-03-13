using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevNoobEdit : MonoBehaviour
{
    public Transform leftShotPos;
    public Transform rightShotPos;
    public GameObject bullet;
    public float reloadTime;
    public bool loaded;
    public GameObject[] Pedestrians;
    public GameObject target;
    public Arrow arrow;

    [Header("Markus")]
    public GameObject MarkusObj;
    public TextMeshProUGUI MarkusText;
    public string[] MarkusSayings;

    public float RewardTime;
    public TimerScript timer;

    private void Start()
    {

        NewTarget();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (loaded)
            {
                loaded = false;
                StartCoroutine(Reloading());
                GameObject sBullet = Instantiate(bullet, leftShotPos.position, Quaternion.identity);
                sBullet.GetComponent<Rigidbody>().AddForce(-transform.right * 1500);
                Destroy(sBullet, 4);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (loaded)
            {
                loaded = false;
                StartCoroutine(Reloading());
                GameObject sBullet = Instantiate(bullet, rightShotPos.position, Quaternion.identity);
                sBullet.GetComponent<Rigidbody>().AddForce(transform.right * 1500);
                Destroy(sBullet, 4);
            }
        }
    }

    IEnumerator Reloading() 
    {
        yield return new WaitForSeconds(reloadTime);
        loaded = true;
    }
    
    public void NewTarget()
    {
        Pedestrians = GameObject.FindGameObjectsWithTag("Pedestrian");
        if (Pedestrians.Length > 0)
        {
            int randval = Random.Range(0, Pedestrians.Length - 1);
            target = Pedestrians[randval];
            arrow.target = target;

            MarkusObj.SetActive(true);
            int saying = Random.Range(0, MarkusSayings.Length);
            MarkusText.text = MarkusSayings[saying];
            StartCoroutine(cancelMarkus());

            RewardTime = Vector3.Distance(transform.position, target.transform.position);
        }
        else
        {
            target = null;
        }
    }

    IEnumerator cancelMarkus()
    {
        yield return new WaitForSeconds(4);
        MarkusObj.SetActive(false);
        int saying = Random.Range(0, MarkusSayings.Length);
        MarkusText.text = "";
    }
}
