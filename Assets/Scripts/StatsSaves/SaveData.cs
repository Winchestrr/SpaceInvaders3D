using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class SaveData
{
    //stats
    public static int score { get; set; }
    public static float roundTime { get; set; }
    public static int enemiesKilled { get; set; }
    public static int chosenShip { get; set; }

    //settings
    public static float musicLevel { get; set; }
    public static float sfxLevel { get; set; }
    public static string graphicsQuality { get; set; }
}
