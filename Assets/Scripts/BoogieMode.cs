using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogieMode : MonoBehaviour
{
    public Light dirLight;
    public AudioSource boogieMusic;

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
        boogieMusic.Play();
        InvokeRepeating("RandomLight", 0, (60f / 132f));
    }

    private void RandomLight()
    {
        dirLight.color = Random.ColorHSV(0, 1, 1, 1, 0, 1);
    }
}
