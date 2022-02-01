using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : GunBase
{
    public Animator animator;

    [Range(0, 1)]
    public float minigunSpeed;

    private void Start()
    {
        isShooting = false;
    }

    private void Update()
    {
        GetInputs();

        animator.SetFloat("MinigunSpeed", minigunSpeed);
        timeBetweenShots /= minigunSpeed;

        
    }

    void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            minigunSpeed = 0.1f;
            StartCoroutine(StartRotating());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }
    }

    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
        shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
    }

    public IEnumerator StartRotating()
    {
        if(isShooting)
        {
            minigunSpeed += minigunSpeed;

            if (minigunSpeed >= 1) minigunSpeed = 1;

            yield return new WaitForSeconds(timeBetweenShots);
            Invoke("StartRotating", 0);
        }

        
    }

    public void StopRotating()
    {
        isShooting = false;
    }
}
