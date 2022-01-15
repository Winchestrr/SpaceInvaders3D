using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Weapons")]
    public GameObject ammoWeaponGO;
    public AmmoWeapon ammoWeapon;
    public GameObject ammoTextGO;
    public Text ammoText;

    [Header("Reload")]
    public GameObject reloadTextGO;
    public Text reloadText;
    public GameObject reloadBarGO;
    public Image reloadBar;

    [Header("Systems")]
    public GameController gameController;
    public Text gameStateText;
    public Text pointsText;
    public GameObject pauseScreen;
    public Image healthBar;
    private dreamloLeaderBoard dreamlo;

    [Header("Game over screen")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Leaderboard")]
    public GameObject leaderboardGO;
    public TextMeshProUGUI lbCount;
    public TextMeshProUGUI lbNames;
    public TextMeshProUGUI lbScore;
    public TextMeshProUGUI lbTime;
    public TMP_InputField nameInput;
    public Button uploadButton;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Instance problem");

        dreamlo = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
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

        if (Input.GetKeyDown(KeyCode.U)) UploadAndGetScores();
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
        finalScoreText.SetText(">final score: {0}", SaveData.finalScore);
    }

    public void UploadAndGetScores()
    {
        StartCoroutine(UploadAndGetScoresIE());
    }

    public IEnumerator UploadAndGetScoresIE()
    {

        uploadButton.interactable = false;
        leaderboardGO.SetActive(true);

        dreamlo.AddScore(
            //GameController.playerName,
            nameInput.text,
            SaveData.finalScore,
            SaveData.enemiesKilled,
            (SaveData.roundTime.ToString() + "s"));

        lbCount.text = "";
        lbNames.text = "Wait...";
        lbScore.text = "";
        lbTime.text = "";

        yield return new WaitForSeconds(0.3f);

        lbNames.text = "";

        List<dreamloLeaderBoard.Score> scoreList = dreamlo.ToListHighToLow();

        if (scoreList == null) yield return null;
        else
        {
            int maxToDisplay = 20;
            int count = 0;

            foreach (dreamloLeaderBoard.Score currentScore in scoreList)
            {
                count++;

                lbCount.text += count + "\n";
                lbNames.text += currentScore.playerName + "\n";
                lbScore.text += currentScore.score + "\n";
                lbTime.text += currentScore.shortText + "\n";

                if (count >= maxToDisplay) break;
            }
        }
    }

    public IEnumerator ReloadBar()
    {
        reloadTextGO.SetActive(false);
        reloadBarGO.SetActive(true);
        reloadBar.fillAmount = 0;

        for (float i = 0; i < 100; i++)
        {
            reloadBar.fillAmount += 0.01f;
            yield return new WaitForSeconds(ammoWeapon.reloadTime / 100);
        }

        reloadBarGO.SetActive(false);
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
