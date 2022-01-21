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
    public LevelController levelController;

    public ParticleSystem contactParticlePrefab;
    private ParticleSystem contactParticle;
    private GameObject particleMover;

        [Header("Stats")]

    public static int maxHealth;
    public static int playerHealth;

    public int obstacleDamage;

    private float targetAngleZ;

    public float movX;
    public static float movZ;
    public static float playerSpeedOut;
    Vector3 direction;

    public float shipPositionClamp;

    private void Awake()
    {
        particleMover = GameObject.Find("ParticleMover");
    }

    private void Start()
    {
        contactParticle = Instantiate(
            contactParticlePrefab,
            transform.position,
            transform.rotation);

        //contactParticle.Stop();
        levelController = FindObjectOfType<LevelController>();

        maxHealth = stats.startHealth;
        //playerHealth = maxHealth;
        playerHealth = 70;

    }

    void LateUpdate()
    {
        targetAngleZ = 0;
        GetInputs();
        PlayerMove();
        SetRotation();
        CheckLimits();

        if (playerHealth <= (stats.startHealth / 3)) StartCoroutine(LowHPSound());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contactCount > 0 && collision.gameObject.tag != "Bullet")
        {
            if (collision.gameObject.layer == 6) return;
            //Debug.Log("particle: " + collision.contacts[0].point.ToString());

            contactParticle.gameObject.transform.position = collision.contacts[0].point;
            contactParticle.gameObject.transform.forward = collision.contacts[0].normal;
            contactParticle.Emit(Random.Range(30, 80));
        }

        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                DealPlayerDamage(obstacleDamage);
                break;

            case "Enemy":
                DealPlayerDamage(collision.gameObject.GetComponent<EnemyBase>().stats.contactDamage);
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
        if (playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }
    }

    private IEnumerator LowHPSound()
    {
        Debug.Log("ie works");

        if(playerHealth <= (stats.startHealth / 3))
        {
            Debug.Log("warunek dziala");

            //SoundManager.PlaySound("lowHP");
            yield return new WaitWhile(() => SoundManager.instance.lowHealth.isPlaying);
            StartCoroutine(LowHPSound());
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

        //do zmiany
        //if(transform.position.x <= 13.5f && transform.position.x >= -13.5f)
        //{
        //    if (direction.magnitude > 0.1f)
        //    {
        //        transform.Translate(direction * stats.speedX * Time.deltaTime, Space.World);
        //    }
        //}

        if (direction.magnitude > 0.1f)
        {
            transform.Translate(direction * stats.speedX * Time.deltaTime, Space.World);
        }

        if (direction.x > 0.2f) targetAngleZ = -stats.desiredAngleZ;
        else if (direction.x < -0.2f) targetAngleZ = stats.desiredAngleZ;
    }

    void SetRotation()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngleZ),
            stats.rotationSpeed * Time.deltaTime);
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

    void CheckLimits()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -shipPositionClamp, shipPositionClamp),
            transform.position.y,
            transform.position.z);
    }
}
