using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public Transform destroyLine;
    public Image healthBar;
    public EnemyStats stats;

    private int currentHealth;
    private bool isAlive = true;
    private bool canMove = true;

    [Header("Raycast")]
    private RaycastHit hitInfo;
    public float rayDistance;
    public bool isPlayerHit;

    private void OnEnable()
    {
        destroyLine = GameObject.Find("DestroyLine").transform;
        currentHealth = stats.maxHealth;
    }

    void Update()
    {
        if(isAlive)
        {
            Move();
            CastRay();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = ((float)currentHealth / (float)stats.maxHealth);

        //do dodania animacja wybuchu
        if (currentHealth <= 0)
        {
            GameController.enemiesKilled++;
            PointsSystem.AddPoints(stats.pointValue);
            Destroy(gameObject);
        }
        
    }

    void Move()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.z -= stats.speed * Time.deltaTime;
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

    private void CastRay()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
        {
            if(hitInfo.transform.gameObject.tag == "Player")
            {
                isPlayerHit = true;
            }
            else
            {
                isPlayerHit = false;
            }
        }
    }
}
