using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyContr : MonoBehaviour
{

    public GameObject explosion;

    private void Awake()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
