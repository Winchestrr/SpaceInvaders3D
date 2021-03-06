using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle toggle;
    public GameObject tutorialWindow;

    private void Awake()
    {
        //Object.DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name == "Settings")
        {
            sfxSlider = GameObject.Find("SFX_slider").GetComponent<Slider>();
            musicSlider = GameObject.Find("Music_slider").GetComponent<Slider>();
            toggle = GameObject.Find("Toggle").GetComponent<Toggle>();

            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

            if (SaveData.isBoogie) toggle.isOn = true;
            else toggle.isOn = false;
        }
    }

    public void OnAnyMenuButton(string name)
    {
        Debug.Log("button");
        SoundManager.PlaySound("click");

        switch(name)
        {
            case "menu":
                Time.timeScale = 1;
                //SceneManager.LoadScene("Main_Menu");
                SceneChange.ChangeScene("menu");
                break;

            case "start":
                //SceneManager.LoadScene("Tile_level");
                SceneChange.ChangeScene("level");
                GameController.currentState = GameController.GameState.BATTLE;
                GameStatsSystem.points = 0;
                EnemySpawner.canSpawn = true;
                Time.timeScale = 1;
                MusicPlayer.MusicSpeedReset();
                break;

            case "credits":
                //SceneManager.LoadScene("Credits");
                SceneChange.ChangeScene("credits");
                break;

            case "settings":
                //SceneManager.LoadScene("Settings");
                SceneChange.ChangeScene("settings");
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

            case "select":
                //SceneManager.LoadScene("Select_ship");
                SceneChange.ChangeScene("select");
                break;

            case "tutorial":
                if (tutorialWindow.activeSelf) tutorialWindow.SetActive(false);
                else tutorialWindow.SetActive(true);
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
                break;

            case "sfx":
                PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
                SaveData.sfxLevel = sfxSlider.value;
                break;
        }
    }

    public void BoogieToggle()
    {
        if (toggle.isOn == true) SaveData.isBoogie = true;
        else SaveData.isBoogie = false;
    }
}
