using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianScript : MonoBehaviour
{
    public float health;
    public Rigidbody[] rb;

    public SkinnedMeshRenderer PedColors;
    public Material[] ShirtColor;

    public DevNoobEdit DevNoob;
    public Collider BoxCol;
    public Animator anim;

    public float walkinSpeed;
    [HideInInspector]
    public float speed;

    public bool canBeAttacked;
    public Transform target;
    public bool walking;

    public GameObject player;
    public TimerScript timer;

    public AudioSource hitAudio;
    public AudioSource wrongDudeAudio;

    private void Start()
    {
        hitAudio = GameObject.FindGameObjectWithTag("hitSound").GetComponent<AudioSource>();
        wrongDudeAudio = GameObject.FindGameObjectWithTag("wrongDude").GetComponent<AudioSource>();
        target = FindClosestEnemy().transform;
        timer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<TimerScript>();
        canBeAttacked = true;
        rb = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rig in rb)
        {
            //rig.useGravity = true;
            //rig.isKinematic = false;
            //rig.mass = 0.001f;
        }

        //CHANGE COLORS//
        Material[] colors = PedColors.materials;

        int randShirt = Random.Range(0, ShirtColor.Length - 1);
        colors[0] = ShirtColor[randShirt];
        PedColors.materials = colors;

        int randPants = Random.Range(0, ShirtColor.Length - 1);
        colors[3] = ShirtColor[randPants];
        PedColors.materials = colors;
        //----------------------//
    }

    private void Update()
    {
        if (walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed * Time.deltaTime);

            Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
            this.transform.LookAt(targetPostition);

            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                speed = 0;
                anim.speed = 0;
            }
            else
            {
                speed = walkinSpeed;
                anim.speed = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            walking = false;
            anim.enabled = false;
            BoxCol.enabled = false;
            Vector3 pointV = (transform.position - collision.transform.position).normalized;
            pointV.y += 2;

            foreach (Rigidbody rig in rb)
            {
                rig.useGravity = true;
                rig.isKinematic = false;
                rig.AddForce(pointV * 50);
            }
            Destroy(collision.gameObject);
            tag = "Untagged";
            SetLayerRecursively(gameObject, 0);



            if (canBeAttacked)
            {
                canBeAttacked = false;
                if (DevNoob.target == gameObject)
                {
                    Debug.Log("Target");
                    DevNoob.NewTarget();
                    Debug.Log(timer.timer);
                    Debug.Log(DevNoob.RewardTime);
                    timer.timer += (DevNoob.RewardTime / 25);
                    Debug.Log(timer.timer);
                    timer.Money += DevNoob.RewardTime;
                    hitAudio.Play();
                }
                else
                {
                    Debug.Log("Not target");
                    timer.timer -= 5;
                    timer.Money -= 5;
                    wrongDudeAudio.Play();
                }
            }
        }
        if (collision.gameObject.CompareTag("CarBod"))
        {
            walking = false;
            anim.enabled = false;
            BoxCol.enabled = false;
            Vector3 pointV = (transform.position - collision.transform.position).normalized;
            pointV.y += 2;

            foreach (Rigidbody rig in rb)
            {
                rig.useGravity = true;
                rig.isKinematic = false;
                rig.AddForce(pointV * 50);
            }
            tag = "Untagged";
            SetLayerRecursively(gameObject, 0);
            gameObject.layer = 0;
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 0;
            }
            if (canBeAttacked)
            {
                canBeAttacked = false;
                if (DevNoob.target == gameObject)
                {
                    Debug.Log("Target");
                    DevNoob.NewTarget();
                    Debug.Log(timer.timer);
                    Debug.Log(DevNoob.RewardTime);
                    timer.timer += (DevNoob.RewardTime / 25);
                    Debug.Log(timer.timer);
                    timer.Money += DevNoob.RewardTime;
                    hitAudio.Play();

                }
                else
                {
                    Debug.Log("Not target");
                    timer.timer -= 5;
                    timer.Money -= 5;
                    wrongDudeAudio.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target") && target == other.transform)
        {
            if (other.GetComponentInParent<TargetScript>().nextTarget.Length > 0)
            {
                int randTar = Random.Range(0, other.GetComponentInParent<TargetScript>().nextTarget.Length - 1);
                target = other.GetComponentInParent<TargetScript>().nextTarget[randTar].transform;
            }
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("target");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

}
