using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator animator;

    public void ChooseShip(int ship)
    {
        SaveData.chosenShip = ship;
    }

    public void OnSceneChange()
    {
        animator.SetTrigger("SceneOut");
    }

    public void ChangeToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void ChangeToGame()
    {
        SceneManager.LoadScene("Tile_level");
    }

    public void ChangeToSelect()
    {
        SceneManager.LoadScene("Select_ship");
    }

    public void ChangeToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ChangeToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
