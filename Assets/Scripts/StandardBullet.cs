using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    protected Rigidbody rb;

    public float lifetime;

    public virtual void Launch(int damage, float speed)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
