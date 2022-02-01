using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    private WeaponSystem weaponSystem;

    [Header("Damage up")]
    public bool isDamageUpActive;
    public float damageUpTime;
    public float damageUpMultiplier;
    
    [Header("Bullets up")]
    public bool isBulletsUpActive;
    public float bulletsUpTime;
    public float bulletsUpMultiplier;

    private void Start()
    {
        weaponSystem = FindObjectOfType<WeaponSystem>();
    }

    public void DamageUp()
    {
        if (isDamageUpActive) return;
        StartCoroutine(DamageUpIE());
    }

    public IEnumerator DamageUpIE()
    {
        isDamageUpActive = true;

        foreach(AmmoWeapon2 weapon in weaponSystem.weapons)
        {
            weapon.damage *= damageUpMultiplier;
        }

        yield return new WaitForSeconds(damageUpTime);

        foreach(AmmoWeapon2 weapon in weaponSystem.weapons)
        {
            weapon.damage /= damageUpMultiplier;
        }
        isDamageUpActive = false;
    }

    public void BulletsUp()
    {
        if (isBulletsUpActive) return;
        StartCoroutine(BulletsUpIE());
    }

    public IEnumerator BulletsUpIE()
    {
        isBulletsUpActive = true;

        foreach (AmmoWeapon2 weapon in weaponSystem.weapons)
        {
            weapon.timeBetweenShots /= bulletsUpMultiplier;
        }

        yield return new WaitForSeconds(bulletsUpTime);

        foreach (AmmoWeapon2 weapon in weaponSystem.weapons)
        {
            weapon.timeBetweenShots *= bulletsUpMultiplier;
        }
        isBulletsUpActive = false;
    }
}
