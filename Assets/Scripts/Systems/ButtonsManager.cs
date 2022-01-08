using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Awake()
    {
        //Object.DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name == "Settings")
        {
            sfxSlider = GameObject.Find("SFX_slider").GetComponent<Slider>();
            musicSlider = GameObject.Find("Music_slider").GetComponent<Slider>();
        }
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

    //public void OnSliderChange(string name, float value)
    //{
    //    switch(name)
    //    {
    //        case "music":
    //            SaveData.musicLevel = value;
    //            break;

    //        case "sfx":
    //            SaveData.sfxLevel = value;
    //            break;
    //    }
    //}
    
    public void OnSliderChange(string name)
    {
        switch (name)
        {
            case "music":
                SaveData.musicLevel = musicSlider.value;
                Debug.Log(SaveData.musicLevel);
                break;

            case "sfx":
                SaveData.sfxLevel = sfxSlider.value;
                Debug.Log(SaveData.sfxLevel);
                break;

        }
    }
}
