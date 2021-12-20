using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : BulletBase
{
    //public virtual void OnTriggerEnter(Collider other)
    //{
    //    Vector3 collisionPoint = other.ClosestPoint(transform.position);
    //    Instantiate(particle, collisionPoint.);

    //    if (other.tag == "Obstacle")
    //    {
    //        Destroy(gameObject);
    //    }

    //    if(other.tag == "Enemy")
    //    {
    //        other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
    //        if(!isPiercing) Destroy(gameObject);
    //    }
    //}

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (haveParticles)
        {
            if (collision.gameObject.tag == "Player") return;
            Vector3 spawnPosition = collision.transform.position;
            Instantiate(particle, spawnPosition, collision.transform.rotation);
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
