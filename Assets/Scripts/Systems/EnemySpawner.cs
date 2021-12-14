using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform leftBorder;
    public Transform rightBorder;

    public GameObject[] entities;

    public static bool canSpawn = true;

    public float spawnTimerMin;
    public float spawnTimerMax;

    public float spawnY;
    public float spawnZ;

    private void Start()
    {
        spawnZ = rightBorder.position.z;

        StartCoroutine(EnemySpawnerCoroutine());
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I)) LineSpawn(false, 0, 3);
    //    if (Input.GetKeyDown(KeyCode.O)) TriangleSpawn(false, 0, 3);
    //    if (Input.GetKeyDown(KeyCode.P)) GuardSpawn(0);
    //    if (Input.GetKeyDown(KeyCode.J)) AskewSpawn(false, 0, 3);
    //    if (Input.GetKeyDown(KeyCode.K)) RandomXTrainSpawn(0, 5);
    //    if (Input.GetKeyDown(KeyCode.L)) RandomGroupSpawn(true, 0, 6);
    //}

    public void SpawnEnemy(bool isRandom, int enemyNo, float posX, float posZ)
    {
        if(canSpawn)
        {
            Vector3 temp = transform.position;
            temp.x = posX;
            temp.y = spawnY;
            temp.z = posZ;

            if(!isRandom) Instantiate(entities[enemyNo], temp, Quaternion.Euler(0, 180, 0));
            else Instantiate(entities[Random.Range(0, entities.Length)], temp, Quaternion.Euler(0, 180, 0));
        }
    }

    #region Patterns

    public void LineSpawn(bool isRandom, int enemyNo, int size)
    {
        //center enemy
        SpawnEnemy(isRandom, 0, (leftBorder.position.x + rightBorder.position.x) / 2, spawnZ);

        for (int i = 1; i < size; i++)
        {
            SpawnEnemy(isRandom, 0, i * 3, spawnZ);
            SpawnEnemy(isRandom, 0, -i * 3, spawnZ);
        }
    }

    public void TriangleSpawn(bool isRandom, int enemyNo, int size)
    {
        //center enemy
        SpawnEnemy(isRandom, 0, (leftBorder.position.x + rightBorder.position.x) / 2, spawnZ);

        for (int i = 1; i < size; i++)
        {
            SpawnEnemy(isRandom, 0, i * 3, spawnZ + i * 3);
            SpawnEnemy(isRandom, 0, -i * 3, spawnZ + i * 3);
        }
    }

    public void GuardSpawn(int enemyNo)
    {
        for (int i = -1; i < 2; i++)
        {
            SpawnEnemy(false, enemyNo, i * 3, spawnZ + i * 3);
        }
        SpawnEnemy(false, enemyNo, -3, spawnZ + 3);
        SpawnEnemy(false, enemyNo, 3, spawnZ - 3);
    }

    public void AskewSpawn(bool isRandom, int enemyNo, int size)
    {
        for (int i = -size; i < size; i++)
        {
            SpawnEnemy(false, enemyNo, i * 3, spawnZ + i * 3);
        }
    }

    public void RandomXTrainSpawn(int enemyNo, int size)
    {
        float temp = Random.Range(leftBorder.position.x, rightBorder.position.x);

        for (int i = 0; i < size; i++)
        {
            SpawnEnemy(false, enemyNo, temp, spawnZ + i * 3);
        }
    }

    public void RandomGroupSpawn(bool isRandom, int enemyNo, int size)
    {
        for (int i = 0; i < size; i++)
        {
            if(isRandom) SpawnEnemy(true, 0, Random.Range(leftBorder.position.x, rightBorder.position.x), spawnZ + i * 4);
            else SpawnEnemy(false, enemyNo, Random.Range(leftBorder.position.x, rightBorder.position.x), spawnZ + i * 4);
        }
    }

    #endregion

    IEnumerator EnemySpawnerCoroutine()
    {
        int formationNo = Random.Range(0, 6);

        bool randomBool = Random.value > 0.5f;
        int randomEnemy = Random.Range(0, entities.Length);

        switch (formationNo)
        {
            case 0:
                LineSpawn(randomBool, randomEnemy, Random.Range(2, 3));
                break;

            case 1:
                TriangleSpawn(randomBool, randomEnemy, Random.Range(2, 3));
                break;

            case 2:
                GuardSpawn(randomEnemy);
                break;

            case 3:
                AskewSpawn(randomBool, randomEnemy, Random.Range(2, 4));
                break;

            case 4:
                RandomXTrainSpawn(randomEnemy, Random.Range(3, 7));
                break;

            case 5:
                RandomGroupSpawn(randomBool, randomEnemy, Random.Range(4, 7));
                break;
        }

        yield return new WaitForSeconds(Random.Range(spawnTimerMin, spawnTimerMax));
        StartCoroutine(EnemySpawnerCoroutine());
    }

    void SpawnEnemies() //old way
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
