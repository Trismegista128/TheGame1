using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rb;
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        var movement = new Vector3(x, y, 0f);

        //rb.(movement * Speed);
        tr.position += movement * Time.deltaTime * Speed;
    }
}
