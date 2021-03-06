 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : StandardBullet
{
    public float findTargetRange;
    public float rotationSpeed;

    public LayerMask targetLayer;
    public Transform target;

    private void Start()
    {
        FindTarget();
    }

    private void Update()
    {
        RotateToTarget();  
    }

    public override void Launch(float _damage, float _speed)
    {
        base.Launch(_damage, _speed);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    void FindTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, findTargetRange, targetLayer);
        Transform closest = null;
        float bestDist = float.MaxValue;

        for(int i = 0; i < cols.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, cols[i].transform.position);

            if(dist < bestDist)
            {
                closest = cols[i].transform;
                bestDist = dist;
            }
        }
        target = closest;
    }

    void RotateToTarget()
    {
        if (target == null)
        {
            return;
        }
        Vector3 toTarget = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.velocity = transform.forward * speed;
    }
}
