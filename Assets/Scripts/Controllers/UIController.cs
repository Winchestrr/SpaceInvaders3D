using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image healthBar;

    private void Start()
    {
        reloadText = reloadTextGO.GetComponent<Text>();
    }

    public void Update()
    {
        SetUI();
    }

    //to zmieni� na static, ale nie wiem jak
    public void SetUI()
    {
        gameStateText.text = GameController.currentState.ToString();

        if(ammoWeaponGO != null)
        {
            ammoText.text = "Ammo: " + ammoWeapon.bulletsLeft + "/" + ammoWeapon.allBullets;
        }

        healthBar.fillAmount = (float)PlayerController.playerHealth / (float)PlayerController.maxHealth;
    }
}
