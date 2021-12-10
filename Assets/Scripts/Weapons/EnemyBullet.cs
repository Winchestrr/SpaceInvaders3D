using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            PlayerController.DealPlayerDamage(damage);
            if (!isPiercing) Destroy(gameObject);
        }
    }
}
