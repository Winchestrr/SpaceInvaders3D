using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform gunEnd;

    [SerializeField] protected float timeBetweenShots;
    protected float lastShotTime;

    public virtual void TryShoot()
    {
        if(CanShoot())
        {
            Shoot();
        }    
    }

    protected virtual void Shoot()
    {
        lastShotTime = Time.time;
        //play gunshot audio
    }

    bool CanShoot()
    {
        if(Time.time > lastShotTime + timeBetweenShots)
        {
            return true;
        }
        return false;
    }
}
