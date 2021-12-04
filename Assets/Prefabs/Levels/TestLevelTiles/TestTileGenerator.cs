using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileGenerator : MonoBehaviour
{
    public GameObject[] subtiles;

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
            GenerateFloor();
            GenerateWalls();
        }
    }

    void GenerateWalls()
    {
        for(int i = 0; i < tileLength; i++)
        {
            for(int j = 0; j < wallHeight; j++)
            {
                Instantiate(subtiles[Random.Range(0, subtiles.Length)], new Vector3(-floorWidth, (2 * j) + 1, (2 * i) + noInOrder), Quaternion.Euler(new Vector3(0, 0, 90)));
                Instantiate(subtiles[Random.Range(0, subtiles.Length)], new Vector3(floorWidth, (2 * j) + 1, (2 * i) + noInOrder), Quaternion.Euler(new Vector3(0, 0, -90)));
            }
        }
    }

    void GenerateFloor()
    {
        for (int i = 0; i < tileLength; i++)
        {
            for (int j = 0; j < floorWidth; j++)
            {
                Instantiate(subtiles[Random.Range(0, subtiles.Length)], new Vector3((2 * j) - floorWidth + 1, 0, (2 * i) + noInOrder), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }
}
