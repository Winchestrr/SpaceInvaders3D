using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> currentLevelTiles = new List<GameObject>();

    public GameObject[] wallsSubtiles;
    public GameObject[] floorSubtiles;
    public GameObject levelTile;
    public GameObject levelGO;

    public Transform destroyLine;

    public int wallSubtileSize;
    public int floorSubtileSize;
    public int tileLength;
    public int floorWidth;
    public int wallHeight;

    private int noInOrder = 0;
    public int numberOfTiles;

    public float levelSpeed;

    private void Start()
    {
        destroyLine = GameObject.Find("DestroyLine").transform;
        GenerateLevel();
    }

    private void Update()
    { 
        MoveLevel();
    }

    #region Generators

    void GenerateLevel()
    {
        for (int x = 0; x < numberOfTiles; x++)
        {
            noInOrder += wallSubtileSize * tileLength;

            GenerateTile(new Vector3(0f, 0f, noInOrder - 1));
        }
    }

    void GenerateTile(Vector3 position)
    {
        GameObject tempTile = Instantiate(levelTile, new Vector3(0, 0, position.z), Quaternion.Euler(new Vector3(0, 0, 0)), levelGO.transform);
        currentLevelTiles.Add(tempTile);

        GenerateFloor(tempTile);
        GenerateWalls(tempTile);
    }

    void GenerateWalls(GameObject parent)
    {
        for(int i = 0; i < tileLength; i++)
        {
            for(int j = 0; j < wallHeight; j++)
            {
                Instantiate(wallsSubtiles[Random.Range(0, wallsSubtiles.Length)],
                    new Vector3(-floorWidth, (wallSubtileSize * j) + 1, (wallSubtileSize * i) + noInOrder),
                    Quaternion.Euler(new Vector3(0, 0, -90)),
                    parent.transform);

                Instantiate(wallsSubtiles[Random.Range(0, wallsSubtiles.Length)],
                    new Vector3(floorWidth, (wallSubtileSize * j) + 1, (wallSubtileSize * i) + noInOrder),
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
                    new Vector3((floorSubtileSize * j) - floorWidth + 1, 0, (floorSubtileSize * i) + noInOrder),
                    Quaternion.Euler(new Vector3(0, 0, 0)),
                    parent.transform);
            }
        }
    }

    #endregion

    void MoveLevel()
    {
        for (int i = 0; i < currentLevelTiles.Count; i++)
        {
            currentLevelTiles[i].transform.Translate(0f, 0f, (levelSpeed - PlayerController.playerSpeedOut) * Time.deltaTime, Space.Self);

            if (currentLevelTiles[i].transform.localPosition.z < -(tileLength * 2))
            //if (currentLevelTiles[i].transform.localPosition.z < destroyLine.transform.position.z)
            {
                Destroy(currentLevelTiles[i]);
                currentLevelTiles.RemoveAt(i);

                //tu jest problem, gdzie dodaje jedn¹ "pust¹" p³ytkê za pierwszym razem (potem jest git)
                GenerateTile(new Vector3(0f, 0f, tileLength * 2 * (numberOfTiles - 1)));
            }
        }
    }
}
