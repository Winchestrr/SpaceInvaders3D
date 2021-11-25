using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] protected Transform gunEnd;
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected ParticleSystem shootParticles;

    [SerializeField] protected int damage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float timeBetweenShots;
    protected float lastShotTime;
    public bool haveParticles;

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

        //if (haveParticles) shootParticles.Play();
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
