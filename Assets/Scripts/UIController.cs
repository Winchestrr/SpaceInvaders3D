using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;
    //do zmiany, �eby bra� aktywny ammo weapon zamiast jednego ca�y czas
    public AmmoWeapon ammoWeapon;

    public Text gameStateText;
    public GameObject ammoTextGO;
    public Text ammoText;

    private void Start()
    {
        //to nie dzia�a
        ammoWeapon = FindObjectOfType<AmmoWeapon>();
    }

    public void Update()
    {
        SetUI();
    }

    public void SetUI()
    {
        gameStateText.text = gameController.currentState.ToString();

        ammoText.text = "Ammo: " + ammoWeapon.bulletsLeft + "/" + ammoWeapon.allBullets;
    }
}
