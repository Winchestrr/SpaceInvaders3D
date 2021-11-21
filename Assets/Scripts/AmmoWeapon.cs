using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon : GunBase
{
    public UIController uiController;

    [SerializeField] protected int magazineSize;
    public int allBullets;
    [SerializeField] protected int ammoCost;
    public int bulletsLeft;

    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool isMagazineEmpty;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        uiController.ammoWeapon = this;
        uiController.ammoWeaponGO = this.gameObject;
    }

    private void OnEnable()
    {
        uiController.ammoTextGO.SetActive(true);
    }

    private void OnDisable()
    {
        uiController.ammoTextGO.SetActive(false);
        uiController.reloadTextGO.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (isMagazineEmpty)
        {
            uiController.reloadTextGO.SetActive(true);
        }
    }

    internal void SetAmount()
    {

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
            uiController.reloadTextGO.SetActive(true);

            yield return new WaitForSeconds(reloadTime);

            allBullets -= magazineSize;
            bulletsLeft = magazineSize;
            isMagazineEmpty = false;
            uiController.reloadTextGO.SetActive(false);
        }
        else { yield break; }
    }
}
