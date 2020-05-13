using UnityEngine;
using TMPro;
using Assets.Scripts.Helpers;

public class Player : MonoBehaviour
{
    public int Lifes;
    public float Speed;
    public float FireRate;
    public float DurationOfImmunityAfterHit;
    public int PlayerSpriteSizeInPixels;
    public GameObject BulletPrefab;
    public TextMeshPro AmmoUI; 

    private Transform tr;
    private SpriteRenderer sr;
    private Animator anim;
    private GameManager manager;
    private HealthBar healthBarObject;
    private float immuneTime;
    private Vector3 lastMovement;
    private int lastDirection = -1;
    private bool primaryWeaponSelected = true;
    private int ammo = 48;

    private ShootingHelper shootingHelper;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();

        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        healthBarObject = GameObject.Find("Life container").GetComponent<HealthBar>();

        AmmoUI.text = AmmoString;
        shootingHelper = new ShootingHelper(tr, BulletPrefab, FireRate);
    }

    private void FixedUpdate()
    {
        if(healthBarObject.IsUpdateRequired)
            healthBarObject.UpdateHealthbar(Lifes);

        if (immuneTime > 0)
            immuneTime -= Time.deltaTime;

        var x = 0;
        var y = 0;

        var moveRight = Input.GetKey(KeyCode.D);
        var moveLeft = Input.GetKey(KeyCode.A);
        var moveUp = Input.GetKey(KeyCode.W);
        var moveDown = Input.GetKey(KeyCode.S);

        x = moveRight ? 1 : moveLeft ? -1 : 0;
        y = moveUp ? 1 : moveDown ? -1 : 0;

        FlipDirection(x);
        SetAnimation(x, y);

        var movement = new Vector3(x, y, 0f);
        var isNoMovement = movement == Vector3.zero;

        if (!isNoMovement)
            lastMovement = movement;

        tr.position += movement * Time.deltaTime * Speed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            primaryWeaponSelected = !primaryWeaponSelected;
            manager.WeaponSwitch(primaryWeaponSelected);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(primaryWeaponSelected)
                shootingHelper.Fire(lastMovement, movement);

            else if(ammo > 0)
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
        if (immuneTime <= 0)
        {
            if (Lifes > 1)
            {
                Lifes--;
                immuneTime = DurationOfImmunityAfterHit;
            }

            else
                manager.GameOver();

            healthBarObject.UpdateHealthbar(Lifes);
        }
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

    private void SetAnimation(int x, int y)
    {
        var hz = anim.GetBool("Horizontal");
        var aU = anim.GetBool("Up");
        var aD = anim.GetBool("Down");

        if (hz != (x != 0)) anim.SetBool("Horizontal", x != 0);
        if (aU != (y == 1)) anim.SetBool("Up", y == 1);
        if (aD != (y == -1)) anim.SetBool("Down", y == -1);

        anim.SetFloat("ImmuneTime", immuneTime);

        if (x == 0 && y == 0 && immuneTime <= 0) anim.speed = 0;
        else anim.speed = 0.5f;
    }

    private string AmmoString => ammo > 9 ? ammo.ToString() : $"0{ammo.ToString()}";
    
}
