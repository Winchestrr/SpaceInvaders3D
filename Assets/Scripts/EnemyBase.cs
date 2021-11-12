using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed;
    public int contactDamage;
    public Transform destroyLine;

    public bool isAlive = true;
    public bool canMove = true;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.z -= speed * Time.deltaTime;
            transform.position = temp;

            if(temp.z < destroyLine.transform.localPosition.z)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        switch(target.tag)
        {
            case "Bullet":
                break;

            case "Enemy":
                break;

            case "Player":
                break;

            case "Obstacle":
                break;
        }
    }
}
