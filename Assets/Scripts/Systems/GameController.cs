using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum GameState { MENU, STARTBATTLE, BATTLE, PAUSE, GAMEOVER }

    [Header("Objects")]
    public CameraController cameraController;
    public GameObject[] playerShips;
    public GameObject currentPlayerShip;
    public CinemachineVirtualCamera virtualCamera;
    public BoogieMode boogie;
    public Animator canvasAnimator;
    public dreamloLeaderBoard dreamlo;


    [Header("Stats")]
    public static GameState currentState;
    public int chosenShipIndex;
    public static int enemiesKilled;

    public static bool isPaused;

    public float playerHeight;

    public static string playerName;

    public delegate void newGameOver();
    public static event newGameOver OnGameOver;


    public void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");

        isPaused = false;

        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;

        GameStatsSystem.enemiesKilled = 0;
        GameStatsSystem.points = 0;
        GameStatsSystem.currentTime = 0;

        chosenShipIndex = SaveData.chosenShip;
        currentPlayerShip = InstantiatePlayerShip(playerShips[chosenShipIndex]);

        if (SaveData.isBoogie) boogie.BoogieModeON();
    }

    private void OnEnable()
    {
        MusicPlayer.MenuMusicPlay(false);

        if (MusicPlayer.instance == null ||
            MusicPlayer.instance.levelMusic.isPlaying == true ||
            SaveData.isBoogie == true)
            return;

        if(!SaveData.isBoogie) MusicPlayer.LevelMusicPlay(true);
    }

    public static void GameOver()
    {
        OnGameOver();

        

        GameController.currentState = GameController.GameState.GAMEOVER;
        EnemySpawner.canSpawn = !EnemySpawner.canSpawn;
        Destroy(instance.currentPlayerShip);
        instance.canvasAnimator.SetTrigger("GameOver");

        SaveData.score = GameStatsSystem.points;
        SaveData.enemiesKilled = GameStatsSystem.enemiesKilled;
        SaveData.roundTime = GameStatsSystem.currentTime;
        SaveData.finalScore = (int) Mathf.Ceil(GameStatsSystem.points +
            (GameStatsSystem.currentTime * Mathf.Ceil(GameStatsSystem.enemiesKilled / 2)));

        SaveGame();

        Time.timeScale = 0.2f;
    }

    GameObject InstantiatePlayerShip(GameObject ship)
    {
        GameObject tempShip;

        tempShip = Instantiate(ship, transform.position + new Vector3(0, playerHeight, 0), transform.rotation);
        StickCameraToPlayer(tempShip);

        return tempShip;
    }

    public void StickCameraToPlayer(GameObject target)
    {
        virtualCamera.Follow = target.transform;
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetFloat("score", SaveData.score);
        PlayerPrefs.SetFloat("roundTime", SaveData.roundTime);
        PlayerPrefs.SetFloat("enemiesKilled", SaveData.enemiesKilled);
        PlayerPrefs.SetFloat("chosenShip", SaveData.chosenShip);

        PlayerPrefs.Save();
    }

    public void UploadScore()
    {
        dreamlo.AddScore(
            playerName,
            SaveData.finalScore,
            SaveData.enemiesKilled,
            (SaveData.roundTime.ToString() + "s"));
    }
}
