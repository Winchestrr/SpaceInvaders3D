using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public float time;
    public int enemiesKilled;

    public SaveData()
    {
        score = GameStatsSystem.points;
        time = GameStatsSystem.currentTime;
        enemiesKilled = GameStatsSystem.enemiesKilled;
    }
}
