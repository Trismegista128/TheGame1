using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public int Lifes;
    public GameObject BulletPrefab;

    private Transform tr;
    private Animator anim;
    private GameManager manager;
    private HealthBar healthBarObject;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        healthBarObject = GameObject.Find("Life container").GetComponent<HealthBar>();
        healthBarObject.UpdateHealthbar(Lifes);
    }

    private void FixedUpdate()
    {
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

        tr.position += movement * Time.deltaTime * Speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = (Instantiate(BulletPrefab, tr.position, new Quaternion())) as GameObject;
            bullet.GetComponent<Bullet>().Initialize(movement);
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
        Debug.Log("Player lost one life");
        if (Lifes > 1)
            Lifes--;
        else
            manager.GameOver();

        healthBarObject.UpdateHealthbar(Lifes);
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
    }
}
