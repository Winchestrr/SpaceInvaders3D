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
    public Animator canvasAnimator;


    [Header("Stats")]
    public static GameState currentState;
    public int chosenShipIndex;
    public static int enemiesKilled;

    public static bool isPaused;

    public float playerHeight;

    public delegate void newGameOver();
    public static event newGameOver OnGameOver;


    public void Awake()
    {
        //instance == null ? instance = this : Debug.LogError("Instance problem"); 

        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");

        isPaused = false;

        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;

        chosenShipIndex = SaveData.chosenShip;
        currentPlayerShip = InstantiatePlayerShip(playerShips[chosenShipIndex]);
    }

    //test
    private void InstantiateControllers()
    {

    }

    public static void GameOver()
    {
        OnGameOver();

        GameController.currentState = GameController.GameState.GAMEOVER;
        EnemySpawner.canSpawn = !EnemySpawner.canSpawn;
        Destroy(instance.currentPlayerShip);
        instance.canvasAnimator.SetTrigger("GameOver");

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
}
