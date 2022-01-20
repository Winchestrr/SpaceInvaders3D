using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int standardAmmo;
    public int doubleAmmo;
    public int homingAmmo;

    private void OnCollisionEnter(Collision collision)
    {
        WeaponSystem weaponSystem = collision.gameObject.GetComponentInChildren<WeaponSystem>();
        AmmoWeapon2 ammoWeapon = weaponSystem.weapons[weaponSystem.currentWeaponIndex].GetComponent<AmmoWeapon2>();

        if (weaponSystem != null)
        {
            ammoWeapon.bulletsLeft += standardAmmo;
            if (ammoWeapon.bulletsLeft >= 999) ammoWeapon.bulletsLeft = 999;
        }

        Destroy(gameObject);
    }
}
