using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { MENU, STARTBATTLE, BATTLE, LOST, WON, PAUSE }
    public GameState currentState;

    private bool isPaused;

    public void Start()
    {
        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;
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
}
