using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentLevelTiles = new List<GameObject>();

    public GameObject[] wallsSubtiles;
    public GameObject[] floorSubtiles;
    public GameObject levelTile;
    public GameObject[] pickups;

    public float wallSubtileSize;
    public float floorSubtileSize;
    public float tileLength;
    public float floorWidth;
    public float wallHeight;

    public int numberOfTiles;
    public int startIndex = -1;

    public static float levelSpeed;
    public float levelSpeedDisplay;

    private Collider[] hitColliders;

    private void Start()
    {
        GenerateLevel();
    }

    private void Update()
    {
        levelSpeed = levelSpeedDisplay;

        MoveLevel();
    }

    #region Generators

    void GenerateLevel()
    {
        for (int x = startIndex; x < numberOfTiles; x++)
        {
            //noInOrder += wallSubtileSize * tileLength; //no nie za bardzo w ten sposób, a nawet jak, to nie mo¿esz odejmowaæ -1 na dole od tego

            //Debug.Log("For x = " + x.ToString() + ", result = " + (x * (wallSubtileSize * tileLength)).ToString());

            GenerateTile(new Vector3(0f, 0f, x * (wallSubtileSize * tileLength))); //najpierw generuj Tile w dobrych miejscach
        }
    }

    void GenerateTile(Vector3 givenPosition)
    {
        //Debug.Log("GenerateTile(" + givenPosition.ToString() + ")");

        //GameObject tempTile = Instantiate(levelTile, new Vector3(0, 0, position.z), Quaternion.Euler(new Vector3(0, 0, 0)), transform); //po co tworzy znów nowy wektor?
        GameObject tempTile = Instantiate(levelTile, givenPosition, Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        currentLevelTiles.Add(tempTile);

        GenerateFloor(tempTile);
        GenerateWalls(tempTile);
        //SpawnPickups(pickups[0]);
    }

    void GenerateWalls(GameObject parent)
    {
        for(int i = 0; i < tileLength; i++)
        {
            for(int j = 0; j < wallHeight; j++)
            {
                Instantiate(wallsSubtiles[Random.Range(0, wallsSubtiles.Length)],
                    //new Vector3(-floorWidth, (wallSubtileSize * j) + 1, (wallSubtileSize * i) + noInOrder), //ten noInOrder trochê Ciê zdradzi³ tutaj
                    new Vector3(-((floorWidth / 2) * floorSubtileSize), (wallSubtileSize * j) + 1, (wallSubtileSize * i)) + parent.transform.position,
                    Quaternion.Euler(new Vector3(0, 0, -90)),
                    parent.transform);

                Instantiate(wallsSubtiles[Random.Range(0, wallsSubtiles.Length)],
                    //new Vector3(floorWidth, (wallSubtileSize * j) + 1, (wallSubtileSize * i) + noInOrder), //ten noInOrder trochê Ciê zdradzi³ tutaj
                    new Vector3(((floorWidth / 2) * floorSubtileSize), (wallSubtileSize * j) + 1, (wallSubtileSize * i)) + parent.transform.position,
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

    void SpawnPickups(GameObject pickup)
    {
        do
        {
            float spawnX = Random.Range(-(floorSubtileSize * floorWidth) / 2, (floorSubtileSize * floorWidth) / 2);
            float spawnZ = Random.Range(-(floorSubtileSize * tileLength) / 2, (floorSubtileSize * tileLength) / 2);
            Vector3 spawnPoint = new Vector3(spawnX, gameObject.transform.position.y, spawnZ);

            hitColliders = Physics.OverlapSphere(spawnPoint, 1);
        }
        while (hitColliders.Length > 0);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i]);
        }
    }

    #endregion

    void MoveLevel()
    {
        for (int i = 0; i < currentLevelTiles.Count; i++)
        {
            currentLevelTiles[i].transform.Translate(0f, 0f, (levelSpeed - PlayerController.playerSpeedOut) * Time.deltaTime, Space.Self);

            //if (currentLevelTiles[i].transform.localPosition.z < -(tileLength * 2))
            if (currentLevelTiles[i].transform.localPosition.z < (-1f + startIndex) * (tileLength * floorSubtileSize))
            //if (currentLevelTiles[i].transform.localPosition.z < destroyLine.transform.position.z)
            {
                Destroy(currentLevelTiles[i]);
                currentLevelTiles.RemoveAt(i);

                //tu jest problem, gdzie dodaje jedn¹ "pust¹" p³ytkê za pierwszym razem (potem jest git)
                GenerateTile(new Vector3(0f, 0f, tileLength * floorSubtileSize * (numberOfTiles - 1)));
            }
        }
    }
}
