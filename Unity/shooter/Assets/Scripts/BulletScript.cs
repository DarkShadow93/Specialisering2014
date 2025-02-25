﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{

    // Public variable 
    public int speed = 1600;
    private Rigidbody2D rb;
    private Enemy enemy;

    // Gets called once when the bullet is created
    void Start () 
    {  
        // Set the Y velocity to make the bullet move upward
        rb = GetComponent<Rigidbody2D>();
        Vector2 dir = transform.right;     
        rb.AddForce(dir * speed);
    }

    // Gets called when the object goes out of the screen
    void OnBecameInvisible() 
    {  
        // Destroy the bullet 
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var name = collider.name;
        if (name == "Enemy")
        {
            enemy = (Enemy) collider.GetComponent(typeof(Enemy));
            enemy.isHit(1);
            Destroy(gameObject);
        }
        if (name != "Player" && name != "Weapon" && name != "Bullet(Clone)")
        {
            Destroy(gameObject);
        }
    }
        
}