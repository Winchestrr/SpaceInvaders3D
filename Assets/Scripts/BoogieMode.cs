using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogieMode : MonoBehaviour
{
    public Light dirLight;
    public AudioSource boogieMusic;
    public GameObject musicPlayer;

    public static bool boogieMode = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BoogieModeON();
        }
    }

    private void BoogieModeON()
    {
        boogieMode = true;
        musicPlayer.SetActive(false);
        boogieMusic.Play();
    }
}
