using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int accel;
    public float topspeed;
    public float destroySpeed;
    public float health;
    public int sensiv;
    public int gravity;
    public GameObject laserBeam;
    public GameObject planet;
    public Slider healthBar;

    private bool onOrbit;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        onOrbit = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mvx = Input.GetAxisRaw("Horizontal");
        float mvy = 0f;
        float mvz = Input.GetAxisRaw("Vertical") + 0.2f;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mvy -= 1f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mvy += 1f;
        }
        /*if (rb.velocity.magnitude > topspeed)
        {
            mvz = 0;
        } */

        Vector3 mv = new Vector3(mvx,mvy,mvz) * accel * Time.deltaTime;
        rb.AddRelativeForce(mv);

        float rty = -Input.GetAxisRaw("Mouse Y");
        float rtx = Input.GetAxisRaw("Mouse X");
        float rtz = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            rtz += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rtz -= 1;
        }
        Vector3 rt = new Vector3(rty*2, rtx, rtz) * sensiv * Time.deltaTime;
        transform.Rotate(rt);

        if (onOrbit)
        {
            Vector3 grav = (planet.transform.position - transform.position).normalized * gravity;
            Physics.gravity = grav;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var lsrBeamObj = Instantiate(laserBeam, transform.position, Quaternion.identity) as GameObject;
            lsrBeamObj.GetComponent<Rigidbody>().velocity = transform.forward.normalized * (rb.velocity.magnitude + 100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            onOrbit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Planet")
        {
            Physics.gravity = new Vector3(0f,0f,0f);
            onOrbit = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Planet")
        {
            if (rb.velocity.magnitude > destroySpeed)
            {
                health -= rb.velocity.magnitude - destroySpeed;
                healthBar.value = health;
                if (health<0)
                { 
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
