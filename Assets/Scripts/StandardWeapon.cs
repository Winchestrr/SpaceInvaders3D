using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : GunBase
{
    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
        shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
    }
}
