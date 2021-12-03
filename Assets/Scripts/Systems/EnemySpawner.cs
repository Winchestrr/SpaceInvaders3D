using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform leftBorder;
    public Transform rightBorder;

    public GameObject[] entities;

    public bool canSpawn = true;

    public float spawnTimerMin;
    public float spawnTimerMax;

    public float spawnY;

    private void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if(canSpawn)
        {
            int randomEnemy = Random.Range(0, entities.Length);

            float posX = Random.Range(leftBorder.position.x, rightBorder.position.x);
            Vector3 temp = transform.position;
            temp.x = posX;
            temp.y = spawnY;

            Instantiate(entities[randomEnemy], temp, Quaternion.Euler(0, 180, 0));

            Invoke("SpawnEnemies", Random.Range(spawnTimerMin, spawnTimerMax));
        }
    }
}
