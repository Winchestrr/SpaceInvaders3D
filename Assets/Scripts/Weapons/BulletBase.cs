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

    [Range(0, 5)]
    public float angle;

    public virtual void Launch(int _damage, float _speed)
    {
        Vector3 bulletDirection = transform.forward;
        bulletDirection.y += Random.Range(-angle, angle);

        rb = GetComponent<Rigidbody>();
        rb.velocity = bulletDirection * _speed;

        speed = _speed;
        damage = _damage;

        Destroy(gameObject, lifetime);
    }
}
