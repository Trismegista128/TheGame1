using UnityEngine;
using TMPro;
using Assets.Scripts.Helpers;
using System;

public class Player : MonoBehaviour
{
    public int Lifes;
    public float Speed;
    public float FireRate;
    public int PlayerSpriteSizeInPixels;
    public GameObject BulletPrefab;
    public TextMeshProUGUI AmmoUI;
    public PlayerAnimation anim;

    private Transform tr;
    private SpriteRenderer sr;
    private GameManager manager;
    private HealthBar healthBarObject;
    private Vector3 lastMovement;
    private int lastDirection = 1;
    private int ammo = 48;
    private bool primaryWeaponSelected = true;
    private bool isImmune = false;

    private ShootingHelper shootingHelper;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        healthBarObject = GameObject.Find("Life container").GetComponent<HealthBar>();

        AmmoUI.text = AmmoString;
        shootingHelper = new ShootingHelper(tr, BulletPrefab, FireRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            primaryWeaponSelected = !primaryWeaponSelected;
            manager.WeaponSwitch(primaryWeaponSelected);
        }
    }

    private void FixedUpdate()
    {
        if (manager.IsPaused) return;
        if (manager.IsGameOver) return;

        if(healthBarObject.IsUpdateRequired)
            healthBarObject.UpdateHealthbar(Lifes);

        var x = 0;
        var y = 0;

        var moveRight = Input.GetKey(KeyCode.D);
        var moveLeft = Input.GetKey(KeyCode.A);
        var moveUp = Input.GetKey(KeyCode.W);
        var moveDown = Input.GetKey(KeyCode.S);

        x = moveRight ? 1 : moveLeft ? -1 : 0;
        y = moveUp ? 1 : moveDown ? -1 : 0;

        FlipDirection(x);

        var movement = new Vector3(x, y, 0f).normalized;
        var isNoMovement = movement == Vector3.zero;

        anim.OnMovement(movement);

        if (!isNoMovement)
            lastMovement = movement;

        tr.position += movement * Time.deltaTime * Speed;

        if (Input.GetKey(KeyCode.Space))
        {
            if (primaryWeaponSelected)
                shootingHelper.Fire(lastMovement, movement);

            else if (ammo > 0)
            {
                var shooted = shootingHelper.Fire(lastMovement, movement);
                if (shooted)
                {
                    ammo--;
                    AmmoUI.text = AmmoString;
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            LoseLife();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            LoseLife();
            Destroy(collision.transform.gameObject);
        }
    }

    private void LoseLife()
    {
        if (!isImmune)
        {
            if (Lifes > 1)
            {
                Lifes--;
                isImmune = true;
                anim.OnHit();
            }

            else
                manager.GameOver();

            healthBarObject.UpdateHealthbar(Lifes);
        }
    }

    public void ImmuneFinished()
    {
        Debug.Log("Immune finished Player");
        isImmune = false;
    }
    private void FlipDirection(int x)
    {
        var direction = x;
        if(lastDirection != direction && direction != 0)
        {
            lastDirection = direction;
            transform.localScale = new Vector2(direction, transform.localScale.y);
        }
    }

    private string AmmoString => ammo > 9 ? ammo.ToString() : $"0{ammo.ToString()}";
    
}
