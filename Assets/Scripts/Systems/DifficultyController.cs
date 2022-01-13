using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public GameObject distanceTracker;

    [SerializeField] private bool isControlled = true;
    [SerializeField] private float distance;
    [Range(0, 3500)]
    [SerializeField] private float debugOffset;

    [SerializeField] private float[] levels;

    private void Update()
    {
        TrackDistance();
        DifficultyChange();
    }

   private void TrackDistance()
    {
        distance = -(distanceTracker.transform.position.z) + debugOffset;
    }

    private void DifficultyChange()
    {
        if(isControlled)
        {
            if (distance <= levels[0])
            {
                LevelController.levelSpeed = -24f;
                EnemySpawner.spawnTimerMin = 3f;
                EnemySpawner.spawnTimerMax = 5f;
            }
            else if (distance <= levels[1])
            {
                LevelController.levelSpeed = -26f;
                EnemySpawner.spawnTimerMin = 2.5f;
                EnemySpawner.spawnTimerMax = 5f;
            }
            else if (distance <= levels[2])
            {
                LevelController.levelSpeed = -30f;
                EnemySpawner.spawnTimerMin = 2f;
                EnemySpawner.spawnTimerMax = 4f;
            }
            else if (distance <= levels[3])
            {
                LevelController.levelSpeed = -34f;
                EnemySpawner.spawnTimerMin = 2f;
                EnemySpawner.spawnTimerMax = 3.5f;
            }
            else if (distance <= levels[4])
            {
                LevelController.levelSpeed = -40f;
                EnemySpawner.spawnTimerMin = 1f;
                EnemySpawner.spawnTimerMax = 2.3f;
            }
        }
    }
}
