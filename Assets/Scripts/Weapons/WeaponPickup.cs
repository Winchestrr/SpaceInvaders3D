using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("dziala");

        WeaponSystem weaponSystem = collision.gameObject.GetComponentInChildren<WeaponSystem>();

        if (weaponSystem != null)
        {
            weaponSystem.AddWeapon(weaponPrefab);
            //animacja znikania / particle
            Destroy(gameObject);
        }
    }
}
