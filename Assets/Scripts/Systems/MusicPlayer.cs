using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    [Header("Audio sources")]
    public AudioSource levelMusic;
    public AudioSource menuMusic;

    [Header("Systems")]
    public AudioLowPassFilter lowPass;
    [Range(0, 5)]
    public float lowPassSpeed;
    [Range(0, 5)]
    public float pitchSpeed;

    public static string previousScene;


    private void Awake()
    {
        

        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);

        LevelMusicPlay(false);

        Debug.Log(menuMusic.isPlaying);

        if (SceneManager.GetActiveScene().name == "Main_Menu" &&
            menuMusic.isPlaying == false &&
            previousScene == "Tile_level")
        {
            MenuMusicPlay(true);
        }
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        MenuMusicPlay(true);
    }

    private void Update()
    {
        levelMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        menuMusic.volume = PlayerPrefs.GetFloat("musicVolume");

        LowpassPitchChange();
    }

    public static void MenuMusicPlay(bool state)
    {
        if (instance == null) return;

        if (state) instance.menuMusic.Play();
        else instance.menuMusic.Stop();
    }

    public static void LevelMusicPlay(bool state)
    {
        if (instance == null) return;

        if (state) instance.levelMusic.Play();
        else instance.levelMusic.Stop();
    }

    void LowpassPitchChange()
    {
        if (GameController.isPaused || (GameController.currentState == GameController.GameState.GAMEOVER))
        {
            lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, 500, lowPassSpeed * Time.fixedDeltaTime);
        }
        else
        {
            lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, 10000, lowPassSpeed * Time.fixedDeltaTime);
        }

        if (GameController.currentState == GameController.GameState.GAMEOVER)
        {
            levelMusic.pitch = Mathf.Lerp(levelMusic.pitch, 0, pitchSpeed * Time.fixedDeltaTime);
        }
    }

    public static void MusicSpeedReset()
    {
        if(instance != null) instance.levelMusic.pitch = 1;
    }
}
