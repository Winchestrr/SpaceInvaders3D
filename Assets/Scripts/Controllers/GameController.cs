using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { MENU, STARTBATTLE, BATTLE, LOST, WON, PAUSE }

    [Header("Objects")]
    //private CameraController cameraController;
    public GameObject[] playerShips;
    public GameObject currentPlayerShip;
    public Transform startPoint;


    [Header("Stats")]
    public GameState currentState;
    public int chosenShipIndex;

    public static bool isPaused;


    public void Start()
    {
        /*
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        if (cameraObject != null)
        {
            cameraController = cameraObject.GetComponent<CameraController>();
        }
        else
        {
            Debug.LogError("cameraObject NOT FOUND!");
        }
        */

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
        GameObject tempShip = Instantiate(ship, startPoint.position, transform.rotation);

        CameraController.StickCameraToPlayer(tempShip.transform);
        
        return tempShip;
    }
}
