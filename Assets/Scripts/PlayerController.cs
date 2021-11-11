using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public CharacterController controller;
    public CameraController cameraController;
    public GameController gameController;
    public WeaponSystem weaponSystem;

    float movX;
    float movZ;
    Vector3 direction;

    [Header("Stats")]
    public float playerSpeed;
    public float playerRotationSpeed;
    public float targetAngleZ;

    private void Update()
    {
        
    }

    void LateUpdate()
    {
        targetAngleZ = 0;
        GetInputs();
        PlayerMove();
        SetRotation();
    }

    void GetInputs()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
            //do zmiany
            weaponSystem.Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameController.GamePause();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponSystem.PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponSystem.NextWeapon();
        }
    }

    void PlayerMove()
    {
        direction = new Vector3(movX, 0, movZ);

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * playerSpeed * Time.deltaTime);

            //to chyba do zmiany bêdzie
            if(Input.GetKey(KeyCode.D))
            {
                targetAngleZ = -30;
            }
            else if(Input.GetKey(KeyCode.A))
            {
                targetAngleZ = 30;
            }
        }
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngleZ), playerRotationSpeed * Time.deltaTime);
    }
}
