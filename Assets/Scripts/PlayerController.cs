using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public CharacterController controller;
    //public CameraController cameraController;
    //public GameController gameController;
    public WeaponSystem weaponSystem;

    float movX;
    float movZ;
    Vector3 direction;

    [Header("Stats")]
    public PlayerStatsSO attachedStats;
    float targetAngleZ;

    public FloatSO playerSpeedOut;

    private void Start()
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
            GameController.GamePause();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponSystem.PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponSystem.NextWeapon();

            Debug.Log(CameraController.SmoothSpeed.ToString());
        }
    }

    void PlayerMove()
    {
        playerSpeedOut.floatValue = movZ * attachedStats.playerSpeed;

        //direction = new Vector3(movX, 0, movZ);
        direction = new Vector3(movX, 0f, 0f);

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * attachedStats.playerSpeed * Time.deltaTime);

            //to chyba do zmiany bêdzie
            if(Input.GetKey(KeyCode.D))
            {
                targetAngleZ = -attachedStats.desiredAngleZ;
            }
            else if(Input.GetKey(KeyCode.A))
            {
                targetAngleZ = attachedStats.desiredAngleZ;
            }
        }
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngleZ), attachedStats.playerRotationSpeed * Time.deltaTime);
    }
}
