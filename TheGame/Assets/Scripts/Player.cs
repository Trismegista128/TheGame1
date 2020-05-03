using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lifes;
    public float Speed;
    public float FireRate;
    public float DurationOfImmunityAfterHit;
    public GameObject BulletPrefab;

    private Transform tr;
    private SpriteRenderer sr;
    private Animator anim;
    private GameManager manager;
    private HealthBar healthBarObject;
    private float lastShoot;
    private float immuneTime;
    private Vector3 lastMovement;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        healthBarObject = GameObject.Find("Life container").GetComponent<HealthBar>();
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

        SetAnimation(x, y);

        var movement = new Vector3(x, y, 0f);
        if (movement != Vector3.zero)
            lastMovement = movement;

        tr.position += movement * Time.deltaTime * Speed;

        if (Input.GetKey(KeyCode.Space))
        {
            Fire(lastMovement, movement);
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

    private void Fire(Vector3 shootDirection, Vector2 currentPlayerMovement)
    {
        if (Time.time > FireRate + lastShoot)
        {
            //deal with the problem that there was no movement yet.
            if (shootDirection == Vector3.zero) shootDirection = new Vector3(-1, 0, 0);

            //deal with the problem that there is no movement now and player was moving diagonaly.
            if (shootDirection.x != 0 && shootDirection.y != 0 && currentPlayerMovement == Vector2.zero)
                shootDirection.y = 0;

            //deal with the problem that bullet is instantiated in wrong place of sprite;
            var positionOfBullet = CalculateBulletStartingPoint(shootDirection);

            var bullet = (Instantiate(BulletPrefab, tr.position + positionOfBullet, new Quaternion())) as GameObject;
            bullet.GetComponent<Bullet>().Initialize(shootDirection);
            lastShoot = Time.time;
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

    private void SetAnimation(int x, int y)
    {
        var aR = anim.GetBool("Right");
        var aL = anim.GetBool("Left");
        var aU = anim.GetBool("Up");
        var aD = anim.GetBool("Down");

        if (aR != (x ==  1)) anim.SetBool("Right", x == 1);
        if (aL != (x == -1)) anim.SetBool("Left", x == -1);
        if (aU != (y ==  1)) anim.SetBool("Up", y == 1);
        if (aD != (y == -1)) anim.SetBool("Down", y == -1);

        anim.SetFloat("ImmuneTime", immuneTime);
    }

    private Vector3 CalculateBulletStartingPoint(Vector3 shootDirection)
    {
        var positionOfBullet = Vector3.zero;
        //Facing left
        if (shootDirection.x < 0)
        {
            //Shooting left-down
            if (shootDirection.y < 0)
            {
                positionOfBullet.x = 0;
                positionOfBullet.y = 1;
            }
            //Shooting left-up
            else if (shootDirection.y > 0)
            {
                positionOfBullet.x = 0;
                positionOfBullet.y = 1;
            }
            //Shooting left
            else
            {
                positionOfBullet.x = 0;
                positionOfBullet.y = 1;
            }
        }
        //Facing right
        else if (shootDirection.x > 0)
        {
            //Shooting right-down
            if (shootDirection.y < 0)
            {
                positionOfBullet.x = 1.33f;
                positionOfBullet.y = 1;
            }
            //Shooting right-up
            else if (shootDirection.y > 0)
            {
                positionOfBullet.x = 1.33f;
                positionOfBullet.y = 1;
            }
            //Shooting right
            else
            {
                positionOfBullet.x = 1.33f;
                positionOfBullet.y = 1;
            }
        }
        //No horizontal facing
        else
        {
            //Shooting Down
            if (shootDirection.y < 0)
            {
                positionOfBullet.x = 0.33f;
                positionOfBullet.y = 1;
            }

            //Shooting Up
            else
            {
                positionOfBullet.x = 1;
                positionOfBullet.y = 2;
            }
        }

        return positionOfBullet;
    }
}
