using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon : GunBase
{
    [SerializeField] protected int magazineSize;
    [SerializeField] protected int allBullets;
    [SerializeField] protected int ammoCost;
    public int bulletsLeft;

    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool isMagazineEmpty;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if(isMagazineEmpty)
        {
            //reload allert in UI controller true
        }
    }

    protected override void Shoot()
    {
        if(bulletsLeft > 0)
        {
            isMagazineEmpty = false;
            bulletsLeft -= ammoCost;

            base.Shoot();
            GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

            if(bulletsLeft == 0)
            {
                isMagazineEmpty = true;
                //reload allert true
            }
        }
    }

    IEnumerator Reload()
    {
        if(allBullets > 0)
        {
            yield return new WaitForSeconds(reloadTime);
            allBullets -= magazineSize;
            bulletsLeft = magazineSize;
            isMagazineEmpty = false;
        }
        else { yield break; }
    }
}
