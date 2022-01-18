using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGun : AmmoWeapon
{
    [SerializeField] private Transform gunEnd2;

    protected override void Shoot()
    {
        if (bulletsLeft > 0)
        {
            isMagazineEmpty = false;
            bulletsLeft -= ammoCost;

            base.Shoot();

            GameObject shotBullet1 = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet1.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

            GameObject shotBullet2 = Instantiate(bullet, gunEnd2.position, transform.rotation);
            shotBullet2.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);


            if (bulletsLeft == 0 && isReloadable)
            {
                isMagazineEmpty = true;
                //reload allert true
            }
            else if(bulletsLeft == 0 && allBullets == 0)
            {
                weaponSystem.RemoveWeapon(gameObject);
                isMagazineEmpty = true;
            }
        }
    }
}
