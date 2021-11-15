using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon : GunBase
{
    [SerializeField] protected int maxAmmo;
    public int currentAmmo;
    [SerializeField] protected int ammoCost;

    void OnEnable()
    {
        
    }

    protected override void Shoot()
    {
        if(currentAmmo > 0)
        {
            currentAmmo -= ammoCost;

            base.Shoot();
            GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
        }
    }
}
