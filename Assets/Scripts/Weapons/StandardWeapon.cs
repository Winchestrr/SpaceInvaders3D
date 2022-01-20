using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : GunBase
{
    protected override void Shoot()
    {
        base.Shoot();

        //SoundManager.PlaySound("shoot");

        GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
        shotBullet.GetComponent<BulletBase>().Launch(damage, bulletSpeed);
    }
}
