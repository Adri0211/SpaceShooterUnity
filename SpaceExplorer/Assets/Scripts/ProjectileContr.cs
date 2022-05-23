using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContr : MonoBehaviour
{

    public GameObject explosion;

    private void Awake()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            var explosionObj = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosionObj, 3);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
