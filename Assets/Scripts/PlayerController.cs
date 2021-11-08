using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    float movX;
    float movZ;
    Vector3 direction;
    public float playerSpeed;

    void Update()
    {
        GetInputs();
        PlayerMove();
    }

    void GetInputs()
    {
        movX = Input.GetAxisRaw("Horizontal");
        movZ = Input.GetAxisRaw("Vertical");
    }

    void PlayerMove()
    {
        direction = new Vector3(movX, 0f, movZ);

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * playerSpeed * Time.deltaTime);
        }

        //add rotation
    }
}
