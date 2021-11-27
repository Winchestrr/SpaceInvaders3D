using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    [Range(0, 1)]
    public float minigunSpeed;
    public Animator animator;

    private void Update()
    {
        animator.SetFloat("MinigunSpeed", minigunSpeed);
    }
}
