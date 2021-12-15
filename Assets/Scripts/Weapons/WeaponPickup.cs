using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    private bool isFound;

    private void Start()
    {
        isFound = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        WeaponSystem weaponSystem = collision.gameObject.GetComponentInChildren<WeaponSystem>();
        
        if (weaponSystem != null)
        {
            foreach (GunBase gunBase in weaponSystem.weapons)
            {
                if(gunBase.gameObject.tag == weaponPrefab.tag)
                {
                    AmmoWeapon tempWeapon = gunBase.gameObject.GetComponent<AmmoWeapon>();
                    tempWeapon.bulletsLeft += tempWeapon.allBullets;

                    isFound = true;
                    Destroy(gameObject);

                    break;
                }
            }

            if (!isFound)
            {
                weaponSystem.AddWeapon(weaponPrefab);
                //animacja znikania / particle
                Destroy(gameObject);
            }
        }
    }
}
