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


    private void Awake()
    {
        levelMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        menuMusic.volume = PlayerPrefs.GetFloat("musicVolume");

        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);

        LevelMusicPlay(false);

        
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu" && menuMusic.isPlaying == false)
        {
            MenuMusicPlay(true);
        }
    }

    private void Update()
    {
        LowpassPitchChange();
    }

    public static void MenuMusicPlay(bool state)
    {
        if (state) instance.menuMusic.Play();
        else instance.menuMusic.Stop();
    }

    public static void LevelMusicPlay(bool state)
    {
        if (state) instance.levelMusic.Play();
        else
        {
            instance.levelMusic.Stop();
        }
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
}
