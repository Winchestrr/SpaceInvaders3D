using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

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

    public GameObject pauseScreen;

    [Header("Game over screen")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");
    }

    #region ---EVENT_ASSIGN---
    private void OnEnable()
    {
        GameController.OnGameOver += SetGameOverScreen;
    }

    private void OnDisable()
    {
        GameController.OnGameOver -= SetGameOverScreen;
    }
    #endregion

    private void Start()
    {
        reloadText = reloadTextGO.GetComponent<Text>();
    }

    public void Update()
    {
        SetUI();
    }

    #region ---SET_UI_ELEMENTS---

    //to zmieniæ na static, ale nie wiem jak
    public void SetUI()
    {
        gameStateText.text = GameController.currentState.ToString();
        pointsText.text = GameStatsSystem.points.ToString();

        if (ammoWeaponGO != null)
        {
            ammoText.text = "Ammo: " + ammoWeapon.bulletsLeft + "/" + ammoWeapon.allBullets;
        }

        healthBar.fillAmount = (float)PlayerController.playerHealth / (float)PlayerController.maxHealth;
    }

    public void SetGameOverScreen()
    {
        scoreText.SetText(">score: {0}", GameStatsSystem.points);
        timeText.SetText(">time : {0}", GameStatsSystem.currentTime);
        enemiesKilledText.SetText(">enemies killed: {0}", GameStatsSystem.enemiesKilled);
        finalScoreText.SetText(">final score: ");
    }

    public static void Pause()
    {
        GameController.isPaused = true;
        Time.timeScale = 0;
        instance.pauseScreen.SetActive(true);
    }

    public static void Unpause()
    {
        GameController.isPaused = false;
        Time.timeScale = 1;
        instance.pauseScreen.SetActive(false);
    }

    #endregion

    #region ---BUTTONS---

    public static void OnRestartButton()
    {
        SceneManager.LoadScene("Tile_level");
        GameController.currentState = GameController.GameState.BATTLE;
        GameStatsSystem.points = 0;
        EnemySpawner.canSpawn = true;
        Time.timeScale = 1;
    }

    public static void OnExitButton()
    {
        Application.Quit();
    }

    public static void OnMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_menu");
    }

    #endregion
}
