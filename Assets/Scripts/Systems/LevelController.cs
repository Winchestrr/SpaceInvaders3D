using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentLevelTiles = new List<GameObject>();

    public GameObject[] wallsSubtiles;
    public GameObject[] floorSubtiles;
    public GameObject[] litSubtiles;
    private GameObject[] currentSubtiles;
    public GameObject levelTile;
    public GameObject[] pickups;
    public GameObject particleMover;

    public float spawnPickupChance;

    public float wallSubtileSize;
    public float floorSubtileSize;
    public float tileLength;
    public float floorWidth;
    public float wallHeight;

    public int numberOfTiles;
    public int startIndex = -1;

    [HideInInspector]
    public float wallPosX;

    public static float levelSpeed;
    public float levelSpeedDisplay;

    private bool isGenerated;
    private bool leftOrRight;

    private Collider[] hitColliders;

    [Header("Light")]
    public GameObject lightLeft;
    public GameObject lightRight;

    //public delegate void SpeedChange(int speed);
    //public static event SpeedChange speedChange;

    private void Start()
    {
        particleMover = GameObject.Find("ParticleMover");
        GenerateLevel();
    }

    private void Update()
    {
        levelSpeedDisplay = levelSpeed;

        MoveLevel();
    }

    #region Generators

    void GenerateLevel()
    {
        for (int x = startIndex; x < numberOfTiles; x++)
        { 
            //Debug.Log("For x = " + x.ToString() + ", result = " + (x * (wallSubtileSize * tileLength)).ToString());

            GenerateTile(new Vector3(0f, 0f, x * (wallSubtileSize * tileLength)));
        }
        isGenerated = true;
    }

    void GenerateTile(Vector3 givenPosition)
    {
        GameObject tempTile = Instantiate(levelTile, givenPosition, Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        currentLevelTiles.Add(tempTile);

        GenerateFloor(tempTile);
        GenerateWalls(tempTile);
        SpawnLight();

        if (isGenerated &&
            pickups.Length > 0 &&
            Random.Range(0, 100) < spawnPickupChance)
        {
            SpawnPickups(pickups[Random.Range(0, pickups.Length)], tempTile.transform);
        }
    }

    void GenerateWalls(GameObject parent)
    {
        for(int i = 0; i < tileLength; i++)
        {
            for(int j = 0; j < wallHeight; j++)
            {
                wallPosX = ((floorWidth / 2) * floorSubtileSize);

                if (Random.Range(0, 100) <= 90) currentSubtiles = wallsSubtiles;
                else currentSubtiles = litSubtiles;

                Instantiate(currentSubtiles[Random.Range(0, currentSubtiles.Length)],
                    new Vector3(-wallPosX, (wallSubtileSize * j) + 1, (wallSubtileSize * i)) + parent.transform.position,
                    Quaternion.Euler(new Vector3(0, 0, -90)),
                    parent.transform);

                Instantiate(currentSubtiles[Random.Range(0, currentSubtiles.Length)],
                    new Vector3(wallPosX, (wallSubtileSize * j) + 1, (wallSubtileSize * i)) + parent.transform.position,
                    Quaternion.Euler(new Vector3(180, 0, 90)),
                    parent.transform);
            }
        }
    }

    void GenerateFloor(GameObject parent)
    {
        for (int i = 0; i < tileLength; i++)
        {
            for (int j = 0; j < floorWidth; j++)
            {
                Instantiate(floorSubtiles[Random.Range(0, floorSubtiles.Length)],
                    //new Vector3((floorSubtileSize * j) - floorWidth + 1, 0, (floorSubtileSize * i) + noInOrder), //ten noInOrder trochê Ciê zdradzi³ tutaj
                    new Vector3((floorSubtileSize * j) - (((floorWidth - 1) / 2f)  * floorSubtileSize), 0, (floorSubtileSize * i)) + parent.transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0)),
                    parent.transform);
            }
        }
    }

    void SpawnPickups(GameObject pickup, Transform parent)
    {
        if (Random.Range(0, 2) > 0)
        {
            float spawnX = Random.Range(-(floorSubtileSize * floorWidth) / 2, (floorSubtileSize * floorWidth) / 2);
            float spawnZ = tileLength * floorSubtileSize * (numberOfTiles - 1);
            Vector3 spawnPoint = new Vector3(spawnX, gameObject.transform.position.y + 5, spawnZ);

            Instantiate(pickup, spawnPoint, transform.rotation, parent);
        }
    }

    void SpawnLight()
    {
        float spawnZ = tileLength * floorSubtileSize * (numberOfTiles - 1);

        Vector3 spawnPointLeft = new Vector3(gameObject.transform.position.x - 1, 27f, spawnZ);
        Vector3 spawnPointRight = new Vector3(gameObject.transform.position.x + 1, 27f, spawnZ);

        if (leftOrRight) Instantiate(lightLeft, spawnPointLeft, lightLeft.transform.rotation, particleMover.transform);
        else Instantiate(lightRight, spawnPointRight, lightRight.transform.rotation, particleMover.transform);

        leftOrRight = !leftOrRight;
    }

    #endregion

    void MoveLevel()
    {
        particleMover.transform.Translate(0f, 0f, (levelSpeed - PlayerController.playerSpeedOut) * Time.deltaTime, Space.Self);

        for (int i = 0; i < currentLevelTiles.Count; i++)
        {
            currentLevelTiles[i].transform.Translate(0f, 0f, (levelSpeed - PlayerController.playerSpeedOut) * Time.deltaTime, Space.Self);

            if (currentLevelTiles[i].transform.localPosition.z < (-1f + startIndex) * (tileLength * floorSubtileSize))
            {
                Destroy(currentLevelTiles[i]);
                currentLevelTiles.RemoveAt(i);

                GenerateTile(new Vector3(0f, 0f, tileLength * floorSubtileSize * (numberOfTiles - 1)));
            }
        }
    }
}
