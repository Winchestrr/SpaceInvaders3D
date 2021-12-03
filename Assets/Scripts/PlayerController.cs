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
    public PlayerStats stats;

    float movX;
    float movZ;
    Vector3 direction;

    [Header("Stats")]

    public static int maxHealth;
    public static int playerHealth;

    float targetAngleZ;

    private void Start()
    {
        maxHealth = stats.startHealth;
        playerHealth = maxHealth;
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
                GameController.GameOver();
                break;

            case "Enemy":
                break;
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        switch(target.tag)
        {
            case "Obstacle":
                Destroy(gameObject);
                GameController.GameOver();
                break;

            case "Enemy":
                PlayerTakeDamage(target.GetComponent<EnemyBase>().stats.contactDamage);
                break;
        }
    }

    public static void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            GameController.GameOver();
        }
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            weaponSystem.NextWeapon();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            //debug player hit
            PlayerTakeDamage(10);
        }
    }

    void PlayerMove()
    {
        direction = new Vector3(movX, 0, movZ);

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * stats.playerSpeed * Time.deltaTime);

            //to chyba do zmiany bêdzie
            if(Input.GetKey(KeyCode.D))
            {
                targetAngleZ = -stats.desiredAngleZ;
            }
            else if(Input.GetKey(KeyCode.A))
            {
                targetAngleZ = stats.desiredAngleZ;
            }
        }
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngleZ), stats.playerRotationSpeed * Time.deltaTime);
    }
}
