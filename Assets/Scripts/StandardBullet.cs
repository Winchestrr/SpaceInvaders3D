using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    protected Rigidbody rb;

    protected int damage;
    protected float speed;
    public float lifetime;

    public virtual void Launch(int _damage, float _speed)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * _speed;

        speed = _speed;
        damage = _damage;

        Destroy(gameObject, lifetime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
