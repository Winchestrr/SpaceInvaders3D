using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun2 : GunBase
{
    public Animator animator;

    [Range(0, 1)] public float minigunSpeed;
    [Range(0, 5)] public float minigunInc;
    [Range(0, 5)] public float minigunDec;
    public float minimumShootingSpeed;

    private void Start()
    {
        isShooting = false;
        minigunSpeed = 0001f;
    }

    private void Update()
    {
        GetInputs();

        animator.SetFloat("MinigunSpeed", minigunSpeed);

        if (isShooting)
        {
            minigunSpeed = Mathf.Lerp(minigunSpeed, 1, minigunInc * Time.deltaTime);
        }
        else
        {
            minigunSpeed = Mathf.Lerp(minigunSpeed, 0.0001f, minigunDec * Time.deltaTime);
        }

        if(minigunSpeed >= minimumShootingSpeed)
        {
            //tu jest problem
            timeBetweenShots = timeBetweenShots / minigunSpeed;
        }
    }

    void GetInputs()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }
    }

    protected override void Shoot()
    {
        base.Shoot();

        //GameObject shotBullet = Instantiate(bullet, gunEnd.position, transform.rotation);
        //shotBullet.GetComponent<StandardBullet>().Launch(damage, bulletSpeed);
    }
}
