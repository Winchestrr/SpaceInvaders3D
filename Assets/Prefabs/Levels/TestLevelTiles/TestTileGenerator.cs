using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileGenerator : MonoBehaviour
{
    public List<GameObject> currentLevelTiles = new List<GameObject>();

    public GameObject[] subtiles;
    public GameObject levelTile;
    public GameObject levelGO;
    private GameObject levelClone;

    public int tileLength;
    public int floorWidth;
    public int wallHeight;

    private int noInOrder = 0;
    public int numberOfTiles;

    public float levelSpeed;

    private void Start()
    {
        levelClone = Instantiate(levelGO, Vector3.zero, Quaternion.identity);
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
            noInOrder += 2 * tileLength;

            GenerateTile(new Vector3(0f, 0f, noInOrder - 1));
        }
    }

    void GenerateTile(Vector3 position)
    {
        GameObject tempTile = Instantiate(levelTile, new Vector3(0, 0, position.z), Quaternion.Euler(new Vector3(0, 0, 0)), levelClone.transform);
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
                Instantiate(subtiles[Random.Range(0, subtiles.Length)],
                    new Vector3(-floorWidth, (2 * j) + 1, (2 * i) + noInOrder),
                    Quaternion.Euler(new Vector3(0, 0, 90)),
                    parent.transform);

                Instantiate(subtiles[Random.Range(0, subtiles.Length)],
                    new Vector3(floorWidth, (2 * j) + 1, (2 * i) + noInOrder),
                    Quaternion.Euler(new Vector3(0, 0, -90)),
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
                Instantiate(subtiles[Random.Range(0, subtiles.Length)],
                    new Vector3((2 * j) - floorWidth + 1, 0, (2 * i) + noInOrder),
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
            {
                Destroy(currentLevelTiles[i]);
                currentLevelTiles.RemoveAt(i);

                //tu jest problem, gdzie dodaje jedn¹ "pust¹" p³ytkê za pierwszym razem (potem jest git)
                GenerateTile(new Vector3(0f, 0f, tileLength * 2 * (numberOfTiles - 1)));
            }
        }
    }
}
