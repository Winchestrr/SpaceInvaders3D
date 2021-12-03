using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLevelController : MonoBehaviour
{
    public GameObject autoLevelTile;
    public float levelTileSize;

    public int levelLength = 10;

    public float levelSpeed = 1f;

    private List<GameObject> currentLevelTiles = new List<GameObject>();
    private Vector3 currentSpawnPoint;

    void OnEnable()
    {
        SpawnLevel();
    }

    void OnDisable()
    {
        ClearLevel();
    }

    void Update()
    {
        MoveLevel();
    }

    private void SpawnLevel()
    {
        currentSpawnPoint = Vector3.zero;

        for (int i = 0; i < levelLength; i++)
        {
            GameObject tempTile = Instantiate(autoLevelTile, currentSpawnPoint, Quaternion.LookRotation(Vector3.up, Vector3.forward), transform);

            currentLevelTiles.Add(tempTile);

            currentSpawnPoint.z += levelTileSize;

            foreach (Transform child in tempTile.transform)
            {
                if (Random.Range(0, tempTile.transform.childCount) < 2)
                {
                    child.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    private void ClearLevel()
    {
        for (int i = currentLevelTiles.Count - 1; i >= 0 ; i--)
        {
            Destroy(currentLevelTiles[i]);
        }
    }

    private void MoveLevel()
    {
        for (int i = 0; i < currentLevelTiles.Count; i++)
        {
            currentLevelTiles[i].transform.Translate(0f, levelSpeed * Time.deltaTime, 0f, Space.Self);

            if (currentLevelTiles[i].transform.localPosition.z < -levelTileSize)
            {
                currentLevelTiles[i].transform.localPosition = new Vector3(0f, 0f, levelTileSize * (levelLength - 1));
            }
        }
    }
}
