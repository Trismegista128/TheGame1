using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            manager.EnemyKilled();
            Destroy(this.gameObject);
        }
    }
}
