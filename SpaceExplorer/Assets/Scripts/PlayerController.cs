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
    public GameObject laserBeam;
    public GameObject planet;
    public Slider healthBar;

    private bool onGround;
    private int bodyMass;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        bodyMass = 30;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mvx = Input.GetAxisRaw("Horizontal");
        float mvy = 0f;
        float mvz = Input.GetAxisRaw("Vertical")+0.2f;
        if (Input.GetKey(KeyCode.LeftControl) && !onGround)
        {
            mvy -= 1.5f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mvy += 1.5f;
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
        if (!onGround)
        {
            Vector3 rt = new Vector3(rty * 2, rtx, rtz) * sensiv * Time.deltaTime;
            transform.Rotate(rt);
        }
        

        if (bodyMass > 0)
        {
            Vector3 grav = (planet.transform.position - transform.position).normalized * bodyMass;
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
            bodyMass = 20;
            planet = other.gameObject;
        }
        if (other.tag == "Star")
        {
            bodyMass = 35;
            planet = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Planet")
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
            bodyMass = 0;
        }
        if (other.tag == "Star")
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
            bodyMass = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Planet")
        {
            onGround = true;
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
        if (collision.collider.tag == "Star")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Planet")
        {
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Planet")
        {
            
            transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
        }
    }
}
