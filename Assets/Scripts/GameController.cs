using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { MENU, STARTBATTLE, BATTLE, LOST, WON, PAUSE }

    [Header("Objects")]
    public CameraController cameraController;
    public GameObject[] playerShips;
    public GameObject currentPlayerShip;
    public Transform startPoint;

    [Header("Stats")]
    public GameState currentState;
    public int chosenShipIndex;
    private bool isPaused;

    public void Awake()
    {
        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;

        currentPlayerShip = InstantiatePlayerShip(playerShips[chosenShipIndex]);
    }

    public void GamePause()
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

        tempShip = Instantiate(ship, startPoint.position, transform.rotation);
        cameraController.StickCameraToPlayer(tempShip.transform);

        return tempShip;
    }
}
