using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public Text gameStateText;

    public void Update()
    {
        SetUI();
    }

    public void SetUI()
    {
        gameStateText.text = gameController.currentState.ToString();
    }
}
