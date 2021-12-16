using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        LaserShot = SFX.Load<AudioCLip>("LaserShot");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound (string clip)
    
        {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(LaserShot);
                break;
        }
        }
}
