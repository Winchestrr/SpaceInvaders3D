using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed;
    public int contactDamage;

    public int maxHealth;
    public int currentHealth;

    public Transform destroyLine;
    public HealthBar healthBar;

    public bool isAlive = true;
    public bool canMove = true;

    private void OnEnable()
    {
        destroyLine = GameObject.Find("EnemyDestroyLine").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHPBar((float)currentHealth / (float)maxHealth);
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
