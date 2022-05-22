using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            //Instantiate()
        }
    }
}
