using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator animator;
    public static string sceneToChange;

    public static SceneChange instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Debug.LogError("Instance problem");
    }

    public void ChooseShip(int ship)
    {
        SaveData.chosenShip = ship;
        PlayerPrefs.SetInt("chosenShip", ship);
    }

    public static void ChangeScene(string scene)
    {
        sceneToChange = scene;
        instance.animator.SetTrigger("ChangeScene");
    }

    public void EndAnimationChangeScene()
    {
        switch(sceneToChange)
        {
            case "menu":
                SceneManager.LoadScene("Main_Menu");
                break;

            case "select":
                SceneManager.LoadScene("Select_ship");
                break;

            case "level":
                SceneManager.LoadScene("Tile_level");
                break;

            case "settings":
                SceneManager.LoadScene("Settings");
                break;

            case "credits":
                SceneManager.LoadScene("Credits");
                break;
        }
    }
}
