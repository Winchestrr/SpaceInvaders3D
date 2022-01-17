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

            case "test":
                Debug.Log("TEST BUTTON");
                break;

            case "save":
                PlayerPrefs.Save();
                break;
        }
    }
    
    public void OnSliderChange(string name)
    {
        switch (name)
        {
            case "music":
                PlayerPrefs.SetFloat("musicVolume", musicSlider.value);

                SaveData.musicLevel = musicSlider.value;
                Debug.Log(SaveData.musicLevel);
                break;

            case "sfx":
                PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);

                SaveData.sfxLevel = sfxSlider.value;
                Debug.Log(SaveData.sfxLevel);
                break;

        }
    }
}