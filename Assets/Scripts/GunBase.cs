using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public GameObject bullet;
    public Transform gunEnd;

    public bool canShoot;
    public float timeBetweenShots;



    void Shoot()
    {

    }

    bool CanShoot()
    {


        return canShoot;
    }
}
