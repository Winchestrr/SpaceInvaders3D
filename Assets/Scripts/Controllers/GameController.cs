using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum GameState { MENU, STARTBATTLE, BATTLE, LOST, WON, PAUSE }

    [Header("Objects")]
    public CameraController cameraController;
    public GameObject[] playerShips;
    public GameObject currentPlayerShip;


    [Header("Stats")]
    public GameState currentState;
    public int chosenShipIndex;

    public static bool isPaused;


    public void Awake()
    {
        //instance == null ? instance = this : Debug.LogError("Instance problem"); 

        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");

        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;

        currentPlayerShip = InstantiatePlayerShip(playerShips[chosenShipIndex]);
    }

    public static void GamePause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    GameObject InstantiatePlayerShip(GameObject ship)
    {
        GameObject tempShip;

        tempShip = Instantiate(ship, transform.position, transform.rotation);
        CameraController.StickCameraToPlayer(tempShip.transform);

        return tempShip;
    }
}
