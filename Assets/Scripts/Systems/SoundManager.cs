using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //szybki tutorial jak dodawa� kolejne d�wi�ki, bo ju� ogarn��em co tam nie dzia�a�o
    //  1. klikasz prawym na obiekt SoundManager na scenie -> create empty
    //  2. nazywasz ten obiekt tak jak nazwa SFXu
    //  3. tworzysz zmienn� public AudioSource *nazwa*;
    //  4. dodajesz case do switcha na wz�r pierwszego
    //  5. zapisujesz kod
    //  6. w inspektorze przeci�gasz plik z d�wi�kiem na stworzony w 1. obiekt (sam stworzy na nim komponent AudioSource)
    //  7. odznaczasz w tym AudioSource play on awake (�eby si� nie uruchamia� przy starcie gry)
    //  8. klikasz na obiekt SoundManager i na puste pole z d�wi�kiem przeci�gasz ca�y obiekt stworzony w 1.
    //     on sam pobierze odpowiedni komponent �eby go wrzuci� do zmiennej
    //  9. �eby sprawdzi� czy dzia�a, mo�esz zrobi� kolejne takie co� jak w Update, tylko zmieni� KeyCode na inny klawisz i play sound na odpowiedni tekst
    //  10. odpalasz giere, klikasz klawisz z 9. i powinno �miga�

    //skrypt SoundManager jest tylko na obiekcie SoundManager. ka�dy kolejny d�wi�k to kolejny obiekt pod SoundManager - te obiekty maj� tylko
    //AudioSource i s� podpi�te do skryptu-matki, kt�ry jest na SoundManagerze

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
