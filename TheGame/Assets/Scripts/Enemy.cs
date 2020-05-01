using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int Lifes;
    public GameObject BulletPrefab;

    private Transform target;
    private Transform tr;
    private GameManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movement = Vector2.MoveTowards(tr.position, target.position, Speed * Time.deltaTime);
        tr.position = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            LoseLife();
            Destroy(collision.transform.gameObject);
        }
    }

    private void LoseLife()
    {
        Debug.Log("Enemy lost one life");
        if (Lifes > 1)
            Lifes--;
        else
            OnKilled();
    }

    private void OnKilled()
    {
        manager.EnemyKilled();
        Destroy(this.gameObject);
    }
}
