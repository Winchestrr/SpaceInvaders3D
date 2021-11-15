using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;
    //do zmiany, ¿eby bra³ aktywny ammo weapon zamiast jednego ca³y czas
    public AmmoWeapon ammoWeapon;

    public Text gameStateText;
    public Text ammoText;

    public void Update()
    {
        SetUI();
    }

    public void SetUI()
    {
        gameStateText.text = gameController.currentState.ToString();

        //ammoText.text = "Ammo: " + ammoWeapon.currentAmmo;
    }
}
