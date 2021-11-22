using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public Transform leftBorder;
    public Transform rightBorder;

    public GameObject[] entities;

    public IEnumerator SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, entities.Length);
        float position = Random.Range(leftBorder.transform.position.x, rightBorder.transform.position.x);

        while(canSpawn)
        {
            Instantiate(entities[randomEnemy], new Vector3(position, 0f, 0f), transform.rotation);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
