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
    public bool isDebug;

    [Header("Raycast")]
    private RaycastHit hitInfo;
    public float rayDistance;
    public bool isPlayerHit;

    public LayerMask castingLayer;

    protected virtual void OnEnable()
    {
        destroyLine = GameObject.Find("DestroyLine").transform;
        currentHealth = stats.maxHealth;
    }

    protected virtual void Update()
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
            GameStatsSystem.enemiesKilled++;
            GameStatsSystem.AddPoints(stats.pointValue);
            Destroy(gameObject);
        }
    }

    protected void Move()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.z -= (stats.speed - LevelController.levelSpeed) * Time.deltaTime;
            transform.position = temp;

            if (temp.z < destroyLine.transform.localPosition.z)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void OnTriggerEnter(Collider target)
    {
        switch(target.tag)
        {
            case "Enemy":
                break;

            case "Obstacle":
                //do zmiany
                Destroy(gameObject);
                break;
        }
    }

    protected void CastRay()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, castingLayer))
        {
            if(isDebug) Debug.Log("hit");

            if(hitInfo.transform.gameObject.tag == "Player") isPlayerHit = true; 
            else isPlayerHit = false;
        }
    }
}
