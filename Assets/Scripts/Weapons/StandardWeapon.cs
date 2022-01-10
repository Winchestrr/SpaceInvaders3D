using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : GunBase
{
    private Quaternion angle;
    public float addAngle = 1;

    protected override void Shoot()
    {
        base.Shoot();

        angle = new Quaternion(1, 1, 1, 1);

        if (Random.Range(0, 1) > 0) angle.x = addAngle;
        else angle.z = addAngle;

        GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation * angle);
        shotBullet.GetComponent<BulletBase>().Launch(damage, bulletSpeed);
    }
}
