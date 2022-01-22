using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private GameObject particleGO;
    private ParticleSystem particle;

    [Header("Ammo pickup")]
    public int standardAmmo;
    public int doubleAmmo;
    public int homingAmmo;

    [Header("Health pickup")]
    public int healingRate;

    public string typeOfPickup;

    private void Start()
    {
        particle = particleGO.GetComponent<ParticleSystem>();
        //particle = Instantiate(particleGO.gameObject, transform.position, transform.rotation).GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(PickUpPickup());
        if (collision.gameObject.tag != "Player") return;

        switch(typeOfPickup)
        {
            case "ammo":
                WeaponSystem weaponSystem = collision.gameObject.GetComponentInChildren<WeaponSystem>();
                AmmoWeapon2 ammoWeapon = weaponSystem.weapons[weaponSystem.currentWeaponIndex].GetComponent<AmmoWeapon2>();

                if (weaponSystem != null)
                {
                    switch(AmmoWeapon2.currentWeaponType)
                    {
                        case "standard":
                            ammoWeapon.bulletsLeft += standardAmmo;
                            break;

                        case "double":
                            ammoWeapon.bulletsLeft += doubleAmmo;
                            break;

                        case "homing":
                            ammoWeapon.bulletsLeft += homingAmmo;
                            break;
                    }

                    if (ammoWeapon.bulletsLeft >= 999) ammoWeapon.bulletsLeft = 999;
                }
                break;

            case "health":
                PlayerController.playerHealth += healingRate;
                break;
        }
    }

    IEnumerator PickUpPickup()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        print("emit particle");
        particle.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
