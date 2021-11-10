using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : GunBase
{
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected int damage;
    [SerializeField] protected float bulletSpeed;

    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
        shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
    }
}
