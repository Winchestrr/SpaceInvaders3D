using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogieMode : MonoBehaviour
{
    public Light dirLight;
    public AudioSource boogieMusic;
    public GameObject musicPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BoogieModeON();
        }
    }

    public void BoogieModeON()
    {
        SaveData.isBoogie = true;
        MusicPlayer.LevelMusicPlay(false);
        boogieMusic.Play();
    }
}
