using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { MENU, STARTBATTLE, BATTLE, LOST, WON }
    public GameState currentState;

    public void Start()
    {
        currentState = GameState.STARTBATTLE;
        currentState = GameState.BATTLE;
    }

    public void Update()
    {
        
    }
}
