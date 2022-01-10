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
    protected float currentTimeBetweenShots;
    protected float lastShotTime;
    public bool haveParticles;
    public bool isShooting = true;

    private void Start()
    {
        currentTimeBetweenShots = timeBetweenShots;
    }

    public virtual void TryShoot()
    {
        Debug.Log("try shoot");

        if(CanShoot())
        {
            Shoot();
        }    
    }

    protected virtual void Shoot()
    {
        lastShotTime = Time.time;
        //play gunshot audio

        if (haveParticles) Instantiate(shootParticles, gunEnd.position, gunEnd.rotation, gunEnd.transform);
    }

    bool CanShoot()
    {
        if(Time.time > lastShotTime + currentTimeBetweenShots && isShooting)
        {
            return true;
        }
        return false;
    }
}
