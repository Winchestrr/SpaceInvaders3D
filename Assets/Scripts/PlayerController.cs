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

        [Header("Stats")]

    public static int maxHealth;
    public static int playerHealth;

    float targetAngleZ;

    public float movX;
    public static float movZ;
    public static float playerSpeedOut;
    Vector3 direction;

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
        Debug.Log("collision");

        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                Destroy(gameObject);
                GameController.GameOver();
                break;

            case "Enemy":
                DealPlayerDamage(collision.gameObject.GetComponent<EnemyBase>().stats.contactDamage);
                Destroy(collision.gameObject);
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
                DealPlayerDamage(target.GetComponent<EnemyBase>().stats.contactDamage);
                break;
        }
    }

    public static void DealPlayerDamage(int damage)
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
            weaponSystem.Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.isPaused) UIController.Unpause();
            else UIController.Pause();
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
            GameController.GameOver();
        }
    }

    void PlayerMove()
    {
        direction = new Vector3(movX, 0, 0);
        playerSpeedOut = stats.speedZ * movZ;

        if (direction.magnitude > 0.1f)
        {
            transform.Translate(direction * stats.speedX * Time.deltaTime, Space.World);
        }

        if (direction.x > 0.2f) targetAngleZ = -stats.desiredAngleZ;
        else if (direction.x < -0.2f) targetAngleZ = stats.desiredAngleZ;
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngleZ), stats.rotationSpeed * Time.deltaTime);
    }

    void OldPlayerMove()
    {
        direction = new Vector3(movX, 0, 0);

        playerSpeedOut = stats.speedZ * movZ;

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * stats.speedX * Time.deltaTime);

            //to chyba do zmiany bêdzie
            if (Input.GetKey(KeyCode.D))
            {
                targetAngleZ = -stats.desiredAngleZ;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                targetAngleZ = stats.desiredAngleZ;
            }
        }
    }
}
