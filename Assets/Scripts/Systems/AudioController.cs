using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
        [Header("Level music")]

    public AudioSource levelMusic;
    public AudioLowPassFilter lowPass;
    [Range(0, 5)]
    public float lowPassSpeed;
    [Range(0, 5)]
    public float pitchSpeed;

        [Header("SFX")]

    public AudioSource testSFX;

    //do Wiktora: jak bêdziesz chcia³ robiæ ten kontroler SFXów to mo¿esz w tym skrypcie,
    //zignoruj to co tu jest bo to tylko do muzyki siê odnosi

    private void Start()
    {
        levelMusic.Play();
    }

    private void Update()
    {
        LowpassPitchChange();
    }

    void LowpassPitchChange()
    {
        if (GameController.isPaused || (GameController.currentState == GameController.GameState.GAMEOVER))
        {
            lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, 500, lowPassSpeed * Time.fixedDeltaTime);
        }
        else
        {
            lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, 10000, lowPassSpeed * Time.fixedDeltaTime);
        }

        if (GameController.currentState == GameController.GameState.GAMEOVER)
        {
            levelMusic.pitch = Mathf.Lerp(levelMusic.pitch, 0, pitchSpeed * Time.fixedDeltaTime);
        }
    }
}
