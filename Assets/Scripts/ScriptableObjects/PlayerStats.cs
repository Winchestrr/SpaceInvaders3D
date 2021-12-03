using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int startHealth;
    public float speedX;
    public float speedZ;
    public float rotationSpeed;
    public float desiredAngleZ;
}
