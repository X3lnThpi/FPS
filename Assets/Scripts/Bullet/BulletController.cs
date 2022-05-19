using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifetime;
    public Rigidbody theRB;
    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}
