using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    public float playerSpeed;
    public float playerRotationSpeed;
    public float desiredAngleZ;
}
