using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    public bool canMove = true;
    public float levelSpeed;

    private void Update()
    {
        LevelMove(levelSpeed);
    }

    void LevelMove(float speed)
    {
        if(canMove)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
