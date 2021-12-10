using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : BulletBase
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

        if(other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            if(!isPiercing) Destroy(gameObject);
        }
    }
}
