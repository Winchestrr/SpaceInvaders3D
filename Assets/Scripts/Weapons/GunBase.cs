using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] protected Transform gunEnd;
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected ParticleSystem shootParticles;
    private ParticleSystem particle;

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
        if(CanShoot()) Shoot();
    }

    protected virtual void Shoot()
    {
        lastShotTime = Time.time;

        //if (haveParticles) Instantiate(shootParticles, gunEnd.position, gunEnd.rotation, gunEnd.transform);

        if (!haveParticles) return;

        if (particle == null)
        {
            particle = Instantiate(shootParticles, gunEnd.position, gunEnd.rotation, gunEnd.transform);
        }

        particle.Emit(20);
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
