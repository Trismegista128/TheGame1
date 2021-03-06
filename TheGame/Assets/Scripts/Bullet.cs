﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    public void Initialize(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * Speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionTag = collision.gameObject.tag;
        DestroyOnHit(collisionTag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.gameObject.tag;
        DestroyOnHit(collisionTag);
    }

    private void DestroyOnHit(string tag)
    {
        if (tag == "Walls" || tag == "Obstacle") Destroy(this.gameObject);
    }
}
