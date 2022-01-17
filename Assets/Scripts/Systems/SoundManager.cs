using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //szybki tutorial jak dodawaæ kolejne dŸwiêki, bo ju¿ ogarn¹³em co tam nie dzia³a³o
    //  1. klikasz prawym na obiekt SoundManager na scenie -> create empty
    //  2. nazywasz ten obiekt tak jak nazwa SFXu
    //  3. tworzysz zmienn¹ public AudioSource *nazwa*;
    //  4. dodajesz case do switcha na wzór pierwszego
    //  5. zapisujesz kod
    //  6. w inspektorze przeci¹gasz plik z dŸwiêkiem na stworzony w 1. obiekt (sam stworzy na nim komponent AudioSource)
    //  7. odznaczasz w tym AudioSource play on awake (¿eby siê nie uruchamia³ przy starcie gry)
    //  8. klikasz na obiekt SoundManager i na puste pole z dŸwiêkiem przeci¹gasz ca³y obiekt stworzony w 1.
    //     on sam pobierze odpowiedni komponent ¿eby go wrzuciæ do zmiennej
    //  9. ¿eby sprawdziæ czy dzia³a, mo¿esz zrobiæ kolejne takie coœ jak w Update, tylko zmieniæ KeyCode na inny klawisz i play sound na odpowiedni tekst
    //  10. odpalasz giere, klikasz klawisz z 9. i powinno œmigaæ

    //skrypt SoundManager jest tylko na obiekcie SoundManager. ka¿dy kolejny dŸwiêk to kolejny obiekt pod SoundManager - te obiekty maj¹ tylko
    //AudioSource i s¹ podpiête do skryptu-matki, który jest na SoundManagerze

    public AudioSource shootSFX;
    public AudioSource lowHealth;

    //public static AudioClip laserShot;
     //AudioSource audioSrc;

    void Start()
    {
      
        // laserShot = SFX.Load<AudioCLip>("LaserShot")
       // audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P)) PlaySound("shoot");
    }
    public void PlaySound (string clip)
    {
        switch (clip)
        {
            case "shoot":
                shootSFX.Play();
                break;

            case "lowHP":
                lowHealth.Play();
                break;
        }
    }
}
