using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : BulletBase
{
    private Transform particleMover;

    public virtual void OnCollisionEnter(Collision collision)
    {
        particleMover = GameObject.Find("ParticleMover").transform;

        if (haveParticles)
        {
            if (collision.gameObject.tag == "Player") return;
            Vector3 spawnPosition = collision.transform.position;
            Instantiate(particle, spawnPosition, collision.transform.rotation, particleMover);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            if (!isPiercing) Destroy(gameObject);
        }
    }
}
