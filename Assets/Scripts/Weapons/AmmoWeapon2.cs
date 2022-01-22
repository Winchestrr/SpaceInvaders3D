using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWeapon2 : GunBase
{
    [Header("Systems")]
    protected WeaponSystem weaponSystem;
    public UIController uiController;

    [Header("General")]
    public bool canReload;
    public static string currentWeaponType;
    public string weaponType;

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

        uiController.ammoWeapon = this;
        uiController.ammoWeaponGO = this.gameObject;
        uiController.canCheckReload = true;

        currentWeaponType = weaponType;
    }

    private void OnEnable()
    {
        uiController.ammoWeapon = this;
        uiController.ammoWeaponGO = this.gameObject;

        if (bulletsInMagazine == 0) uiController.reloadTextGO.SetActive(true);
    }

    private void OnDisable()
    {
        if (uiController.reloadTextGO != null) uiController.reloadTextGO.SetActive(false);

        if (!canReload && uiController != null)
        {
            uiController.StopReload();
            canReload = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isReloadable && canReload)
        {
            StartCoroutine(Reload());
        }

        if(isMagazineEmpty && isReloadable &&uiController.canCheckReload)
        {
            uiController.reloadTextGO.SetActive(true);
        }
    }

    protected override void Shoot()
    {
        if (bulletsInMagazine > 0)
        {
            if (weaponType == "homing") SoundManager.PlaySound("homing");
            else SoundManager.PlaySound("shoot");

            bulletsInMagazine--;

            base.Shoot();

            GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
            shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);

            if (bulletsInMagazine <= 0 && isReloadable)
            {
                bulletsInMagazine = 0;
                isMagazineEmpty = true;
            }
        }
    }

    IEnumerator Reload()
    {
        if (bulletsLeft > 0 && isReloadable)
        {
            isShooting = false;
            canReload = false;

            yield return StartCoroutine(uiController.ReloadBar());
            //yield return new WaitForSeconds(reloadTime);

            if(bulletsLeft <= magazineSize)
            {
                bulletsInMagazine += bulletsLeft;
                bulletsLeft = 0;
            }
            else
            {
                bulletsLeft -= (magazineSize - bulletsInMagazine);
                bulletsInMagazine += (magazineSize - bulletsInMagazine);
            }
            
            isMagazineEmpty = false;

            if (bulletsLeft <= 0) bulletsLeft = 0;

            isShooting = true;
            canReload = true;
        }
        else yield break;
    }
}
