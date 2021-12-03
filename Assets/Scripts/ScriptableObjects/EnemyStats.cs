using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float speed;
    public int contactDamage;
    public int maxHealth;
    public int pointValue;
}
