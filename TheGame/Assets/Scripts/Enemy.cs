using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int Lifes;

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
        tr.position = Vector2.MoveTowards(tr.position, target.position, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Lifes > 1) LoseLife();
            else OnKilled();
        }
    }

    private void LoseLife()
    {
        Lifes--;
        Debug.Log("Enemy lost life");
    }

    private void OnKilled()
    {
        manager.EnemyKilled();
        Destroy(this.gameObject);
    }
}
