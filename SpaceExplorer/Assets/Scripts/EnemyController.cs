using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float accel;
    public float maxSpeed;
    public float fireRate;
    public GameObject player;
    public GameObject laserBeam;

    private Rigidbody rb;
    private bool combatMode;
    private float timeToFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        combatMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (rb.velocity.magnitude + (transform.forward.normalized * Time.deltaTime * accel).magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward.normalized * Time.deltaTime * accel);
        } 

        if (combatMode)
        {
            if (Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;

                var lsrBeamObj = Instantiate(laserBeam, transform.position + (transform.forward.normalized * 3), Quaternion.identity) as GameObject;
                lsrBeamObj.GetComponent<Rigidbody>().velocity = transform.forward.normalized * (rb.velocity.magnitude + 100);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            combatMode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            combatMode = false;
        }
    }
}
