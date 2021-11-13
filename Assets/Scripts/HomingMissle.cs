using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : StandardBullet
{
    private void Update()
    {
        SetAngle(SetTarget());
    }

    public override void Launch(int damage, float speed)
    {
        base.Launch(damage, speed);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    GameObject SetTarget()
    {
        GameObject target;


        return target;
    }

    void SetAngle(GameObject target)
    {

    }
}
