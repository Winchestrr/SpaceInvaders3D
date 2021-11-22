using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem shootingParticles;

    public void ShootParticles()
    {
        shootingParticles.Play();
    }
}
