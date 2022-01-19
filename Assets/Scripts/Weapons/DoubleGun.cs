using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGun : AmmoWeapon2
{
    [SerializeField] private Transform gunEnd2;

    protected override void Shoot()
    {
        if (bulletsLeft > 0)
        {
            isMagazineEmpty = false;
            bulletsLeft--;

            base.Shoot();

            GameObject shotBullet1 = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet1.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

            GameObject shotBullet2 = Instantiate(bullet, gunEnd2.position, transform.rotation);
            shotBullet2.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);


            if (bulletsInMagazine <= 0 && isReloadable)
            {
                bulletsInMagazine = 0;
                isMagazineEmpty = true;

                if (bulletsLeft <= 0)
                {
                    Debug.Log("remove weapon");
                    weaponSystem.RemoveWeapon(this.gameObject);
                }
            }
        }
    }
}
