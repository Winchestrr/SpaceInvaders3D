using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public GameObject ammoWeaponGO;
    public AmmoWeapon ammoWeapon;

    public GameObject ammoTextGO;
    public Text ammoText;

    public GameObject reloadTextGO;
    public Text reloadText;

    public Text gameStateText;
    public Text pointsText;

    public Image healthBar;

    private void Start()
    {
        reloadText = reloadTextGO.GetComponent<Text>();
    }

    public void Update()
    {
        SetUI();
    }

    //to zmieniæ na static, ale nie wiem jak
    public void SetUI()
    {
        gameStateText.text = GameController.currentState.ToString();
        pointsText.text = PointsSystem.points.ToString();

        if (ammoWeaponGO != null)
        {
            ammoText.text = "Ammo: " + ammoWeapon.bulletsLeft + "/" + ammoWeapon.allBullets;
        }

        healthBar.fillAmount = (float)PlayerController.playerHealth / (float)PlayerController.maxHealth;
    }

    public static void OnRestartButton()
    {
        SceneManager.LoadScene("Tile_level");
        GameController.currentState = GameController.GameState.BATTLE;
        PointsSystem.points = 0;
        Time.timeScale = 1;
    }

    public static void OnExitButton()
    {
        Application.Quit();
    }

    public static void OnMenuButton()
    {
        //load menu scene
    }
}
