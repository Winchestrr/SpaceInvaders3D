using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon : GunBase
{
    public UIController uiController;
    public WeaponSystem weaponSystem;

    [SerializeField] protected bool isReloadable;
    public int magazineSize;
    public int allBullets;
    [SerializeField] protected int ammoCost;
    public int bulletsLeft;

    public float currentReloadRate;
    public float reloadTime;
    [SerializeField] protected bool isMagazineEmpty;

    public bool canReload;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        weaponSystem = FindObjectOfType<WeaponSystem>();
        //uiController.ammoWeapon = this;
        //uiController.ammoWeaponGO = this.gameObject;
        uiController.canCheckReload = true;

        
    }

    private void OnEnable()
    {
        uiController.ammoCounterGO.SetActive(true);

        canReload = true;
    }

    private void OnDisable()
    {
        if(uiController.ammoCounterGO != null)
        {
            uiController.ammoCounterGO.SetActive(false);
        }

        if(uiController.reloadTextGO != null)
        {
            uiController.reloadTextGO.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isReloadable && canReload)
        {
            StartCoroutine(Reload());
        }

        if (isMagazineEmpty && isReloadable && uiController.canCheckReload)
        {
            uiController.reloadTextGO.SetActive(true);
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

            if(bulletsLeft == 0 && isReloadable)
            {
                isMagazineEmpty = true;
                //reload allert true
            }

            if(allBullets <= 0) weaponSystem.RemoveWeapon(this.gameObject);
        }
    }

    IEnumerator Reload()
    {
        if(allBullets > 0 && isReloadable)
        {
            isShooting = false;
            canReload = false;
            uiController.reloadTextGO.SetActive(false);

            yield return StartCoroutine(uiController.ReloadBar());

            allBullets -= (magazineSize - bulletsLeft);
            bulletsLeft = magazineSize;
            isMagazineEmpty = false;

            if (allBullets <= 0) allBullets = 0;

            isShooting = true;
            canReload = true;
        }
        else yield break;
    }
}
