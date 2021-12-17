using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class SaveData
{
    public static int score { get; set; }
    public static float roundTime { get; set; }
    public static int enemiesKilled { get; set; }
    public static int chosenShip { get; set; }

    //public SaveData()
    //{
    //    score = GameStatsSystem.points;
    //    time = GameStatsSystem.currentTime;
    //    enemiesKilled = GameStatsSystem.enemiesKilled;
    //}
}
