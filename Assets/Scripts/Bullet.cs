using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D bulletRigidbody;
    [SerializeField] float bulletSpeed = 40f;
    //object player from class PlayerMovement
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        //we want ot do this only in start
        player = FindObjectOfType<PlayerMovement>();
        //its on player in transform, in local scale only X axis, 
        //multiply by buletspeed
        xSpeed = player.transform.localScale.x*bulletSpeed;
    }

    void Update()
    {
        bulletRigidbody.velocity = new Vector2(xSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {//if bullet collides with object of tag enemy
        //we will call it other
        if (other.tag == "Enemy")
        { //destroy that other object
            Destroy(other.gameObject);
        }
        //destroy the bullet/ this game Object
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {//its basically if you hit anything
         Destroy(gameObject); 
    }
}
