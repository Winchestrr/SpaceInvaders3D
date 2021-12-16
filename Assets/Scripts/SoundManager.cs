using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource shootSFX;

    //public static AudioClip laserShot;
     //AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
      
        // laserShot = SFX.Load<AudioCLip>("LaserShot")
       // audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PlaySound("shoot");

    }
    public void PlaySound (string clip)
    
        {
        switch (clip)
        {
            case "shoot":
                shootSFX.Play();
                break;

            
        }
        }
}
