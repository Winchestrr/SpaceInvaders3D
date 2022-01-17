using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStatsSystem : MonoBehaviour
{
    public static GameStatsSystem instance;

    public dreamloLeaderBoard dreamlo;

    public static int points;
    public int pointsDisplay;

    public static int enemiesKilled;
    public int enemiesKilledDisplay;

    private bool canCount;
    public static float startTime = 0;
    public static float currentTime;
    public static float gameOverTime;

    private void Start()
    {
        if (instance == null) instance = this;
        canCount = true;
    }

    private void Update()
    {
        CountTime();
    }

    public static void AddPoints(int value)
    {
        points += value;
        instance.pointsDisplay = points;
    }

    private void CountTime()
    {
        if(canCount)
        {
            startTime += Time.deltaTime;
            currentTime = Mathf.Round(startTime * 100f) * 0.01f;
        }
    }
}
