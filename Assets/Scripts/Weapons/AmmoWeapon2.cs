using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon2 : GunBase
{
    [Header("Systems")]
    private WeaponSystem weaponSystem;
    private UIController uiController;

    [Header("General")]
    public bool canReload;

    [Header("Bullets")]
    public int bulletsInMagazine;
    public int bulletsLeft;
    public int magazineSize;
    public bool isMagazineEmpty;

    [Header("Reload")]
    public float reloadTime;
    public bool isReloadable;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        weaponSystem = FindObjectOfType<WeaponSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isReloadable && canReload)
        {
            StartCoroutine(Reload());
        }
    }

    protected override void Shoot()
    {
        if (bulletsInMagazine > 0)
        {
            bulletsInMagazine--;

            base.Shoot();

            GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

            if (bulletsInMagazine <= 0 && isReloadable)
            {
                bulletsInMagazine = 0;
                isMagazineEmpty = true;

                if(bulletsLeft <= 0)
                {
                    Debug.Log("remove weapon");
                    //weaponSystem.RemoveWeapon(this.gameObject);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        if (bulletsLeft > 0 && isReloadable)
        {
            isShooting = false;
            canReload = false;

            //yield return StartCoroutine(uiController.ReloadBar());
            yield return new WaitForSeconds(reloadTime);

            bulletsLeft -= (magazineSize - bulletsInMagazine);
            bulletsInMagazine = (magazineSize - bulletsInMagazine);
            isMagazineEmpty = false;

            if (bulletsLeft <= 0) bulletsLeft = 0;

            isShooting = true;
            canReload = true;
        }
        else yield break;
    }
}
