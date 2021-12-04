using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileGenerator : MonoBehaviour
{
    private List<GameObject> currentLevelTiles = new List<GameObject>();

    public GameObject[] subtiles;
    public GameObject levelTile;

    public int tileLength;
    public int floorWidth;
    public int wallHeight;

    private int noInOrder = 0;
    public int numberOfTiles;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GenerateLevel();
        }
    }

    void GenerateLevel()
    {
        for (int x = 0; x < numberOfTiles; x++)
        {
            noInOrder += 2 * tileLength;

            float tempZ = noInOrder - 1;
            GameObject tempTile = Instantiate(levelTile, new Vector3(0, 0, tempZ), Quaternion.Euler(new Vector3(0, 0, 0)));
            currentLevelTiles.Add(tempTile);
            
            GenerateFloor(tempTile);
            GenerateWalls(tempTile);
        }
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
}
