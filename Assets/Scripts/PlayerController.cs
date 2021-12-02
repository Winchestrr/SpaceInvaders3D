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

    [SerializeField] private int startHealth;
    public static int playerHealth;

    public float playerSpeed;
    public float playerRotationSpeed;
    public float desiredAngleZ;
    float targetAngleZ;

    private void Start()
    {
        playerHealth = startHealth;
    }

    void LateUpdate()
    {
        targetAngleZ = 0;
        GetInputs();
        PlayerMove();
        SetRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Obstacle":
                Destroy(gameObject);
                GameController.currentState = GameController.GameState.GAMEOVER;
                break;
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        switch (target.tag)
        {
            case "Obstacle":
                Destroy(gameObject);
                GameController.GameOver();
                break;
        }
    }

    public static void DealPlayerDamage(int damage)
    {
        playerHealth -= damage;
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
                targetAngleZ = -desiredAngleZ;
            }
            else if(Input.GetKey(KeyCode.A))
            {
                targetAngleZ = desiredAngleZ;
            }
        }
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngleZ), playerRotationSpeed * Time.deltaTime);
    }
}
