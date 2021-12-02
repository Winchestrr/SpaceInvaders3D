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

    private void Start()
    {
        destroyLine = GameObject.Find("EnemyDestroyLine").transform;
    }

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

            if (temp.z < destroyLine.transform.localPosition.z)
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
                //do dodania animacja niszczenia
                Destroy(gameObject);
                break;

            case "Enemy":
                break;

            case "Player":
                //do dodania obra¿enia dla bohatera
                Destroy(gameObject);
                break;

            case "Obstacle":
                //do zmiany
                Destroy(gameObject);
                break;
        }
    }
}
