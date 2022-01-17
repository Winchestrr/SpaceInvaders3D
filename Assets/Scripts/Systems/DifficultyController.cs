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

    //[SerializeField] private float[] levels;
    //[SerializeField] private float[] levelSpeedValues;
    //[SerializeField] private float[] spawnTimerMinValues;
    //[SerializeField] private float[] spawnTimerMaxValues;

    [SerializeField] private DiffProperties[] diffProps;


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
        //if(isControlled)
        //{
        //    if (distance <= levels[0])
        //    {
        //        SetDifficulty(0);
        //    }
        //    else if (distance <= levels[1])
        //    {
        //        SetDifficulty(1);
        //    }
        //    else if (distance <= levels[2])
        //    {
        //        SetDifficulty(2);
        //    }
        //    else if (distance <= levels[3])
        //    {
        //        SetDifficulty(3);
        //    }
        //    else if (distance <= levels[4])
        //    {
        //        SetDifficulty(4);
        //    }
        //}

        if(isControlled)
        {
            foreach (DiffProperties prop in diffProps)
            {
                if (distance < prop.levelDistance)
                {
                    SetDifficulty(prop);
                    break;
                }
            }
        }
    }

    private void SetDifficulty(DiffProperties prop)
    {
        LevelController.levelSpeed = prop.levelSpeedValue;
        EnemySpawner.spawnTimerMin = prop.spawnTimerRange.x;
        EnemySpawner.spawnTimerMax = prop.spawnTimerRange.y;
    }
}

[System.Serializable]
public class DiffProperties
{
    public float levelDistance;
    public float levelSpeedValue;
    public Vector2 spawnTimerRange;
}