using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGun : GunBase
{
    [SerializeField] private Transform gunEnd2;

    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotBullet1 = Instantiate(bullet, gunEnd.position, transform.rotation);
        shotBullet1.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

        GameObject shotBullet2 = Instantiate(bullet, gunEnd2.position, transform.rotation);
        shotBullet2.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
    }
}
