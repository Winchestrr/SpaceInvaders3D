using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public List<GunBase> weapons;
    public Transform weaponHolder;

    int currentWeaponIndex;

    private void Start()
    {
        GunBase[] tmpWeapons = GetComponentsInChildren<GunBase>(true);
        foreach(GunBase weapon in tmpWeapons)
        {
            weapons.Add(weapon);
        }
    }

    public void Shoot()
    {
        weapons[currentWeaponIndex].TryShoot();
    }

    public void SetWeapon(int newWeaponIndex)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        currentWeaponIndex = newWeaponIndex;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

        if(weapons[currentWeaponIndex] is AmmoWeapon ammoWeapon)
        {
            ammoWeapon.SetAmount();
        }
    }

    public void NextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1;
        if(nextIndex >= weapons.Count)
        {
            nextIndex = 0;
        }
        SetWeapon(nextIndex);
    }

    public void PreviousWeapon()
    {
        int previousIndex = currentWeaponIndex - 1;
        if(previousIndex < 0)
        {
            previousIndex = weapons.Count - 1;
        }
        SetWeapon(previousIndex);
    }

    public void AddWeapon(GameObject weapon)
    {
        GameObject newWeaponObject = Instantiate(weapon, weaponHolder);
        weapons.Add(newWeaponObject.GetComponent<GunBase>());
        newWeaponObject.SetActive(false);
    }

    public void RemoveWeapon(GameObject weapon)
    {
        currentWeaponIndex--;
        weapons.Remove(weapon.GetComponent<GunBase>());
        Destroy(weapon);
    }
}
