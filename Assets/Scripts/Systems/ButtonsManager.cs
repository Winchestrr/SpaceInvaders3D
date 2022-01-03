using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    private void Awake()
    {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void OnAnyMenuButton(string name)
    {
        switch(name)
        {
            case "menu":
                Time.timeScale = 1;
                SceneManager.LoadScene("Main_Menu");
                break;

            case "start":
                SceneManager.LoadScene("Tile_level");
                GameController.currentState = GameController.GameState.BATTLE;
                GameStatsSystem.points = 0;
                EnemySpawner.canSpawn = true;
                Time.timeScale = 1;
                break;

            case "credits":
                SceneManager.LoadScene("Credits");
                break;

            case "settings":
                SceneManager.LoadScene("Settings");
                break;

            case "exit":
                Application.Quit();
                break;
        }
    }
}
