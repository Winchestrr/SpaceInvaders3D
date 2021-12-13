using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameStatsSystem : MonoBehaviour
{
    public static GameStatsSystem instance;

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

[System.Serializable]
public class PlayerData
{
    public int score;
    public float time;
    public int enemiesKilled;

    public PlayerData(GameStatsSystem stats)
    {
        score = GameStatsSystem.points;
        time = GameStatsSystem.currentTime;
        enemiesKilled = GameStatsSystem.enemiesKilled;
    }
}

public static class SaveSystem
{
    public static void SavePlayer(GameStatsSystem stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.stats";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(stats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.stats";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
}
