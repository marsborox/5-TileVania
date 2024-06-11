using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemyMoveSpeed = 1f; 
    Rigidbody2D enemyRigidBody;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    { 
        enemyRigidBody.velocity = new Vector2(enemyMoveSpeed, 0); 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    { //   from here it says what direction are you facing - between *
        //better said whichever direction you are facing flip it
        transform.localScale = new Vector2(-Mathf.Sign(enemyRigidBody.velocity.x), 1f);
    }
}
