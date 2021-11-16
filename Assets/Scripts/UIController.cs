using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;
    //do zmiany, ¿eby bra³ aktywny ammo weapon zamiast jednego ca³y czas
    public GameObject ammoWeaponGO;
    public AmmoWeapon ammoWeapon;

    public Text gameStateText;
   // public GameObject ammoTextGO;
    public Text ammoText;

    public void Update()
    {
        SetUI();
    }

    public void SetUI()
    {
        gameStateText.text = gameController.currentState.ToString();

        if(ammoWeaponGO != null)
        {
            ammoText.text = "Ammo: " + ammoWeapon.bulletsLeft + "/" + ammoWeapon.allBullets;
        }
    }
}
