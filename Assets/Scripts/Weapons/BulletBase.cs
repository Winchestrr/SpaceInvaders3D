using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected Rigidbody rb;
    public ParticleSystem particle;

    protected int damage;
    protected float speed;
    public float lifetime;

    public bool haveParticles = false;
    public bool isPiercing = false;

    public virtual void Launch(int _damage, float _speed)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * _speed;

        speed = _speed;
        damage = _damage;

        Destroy(gameObject, lifetime);
    }
}
