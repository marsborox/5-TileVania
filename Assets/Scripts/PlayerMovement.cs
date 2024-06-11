using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float climbSpeed = 5f;

    //this is bullet
    [SerializeField] GameObject bullet;
    //this is where it spawns
    [SerializeField] Transform gun;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;


    bool isAlive;
    [SerializeField] Vector2 deathKick = new Vector2(40f,25f);

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        //take gravity from start
        gravityScaleAtStart = myRigidbody.gravityScale;

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
         
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        Debug.Log(isAlive);
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump (InputValue value) 
    {
        if (!isAlive) { return; }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        { //into () we are adding (Xvalue, Yvalue) so keep running speed jsut go up as well
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void ClimbLadder()
    {//can climb only when on ladder - if you are not on ladder set gravity at start and nothign return  
        //we swapped legs for body here
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {   //set what is starting gravity 
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }
        //set vertical sped when climbing and pressing button
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;
            //this is for if on ladder not do climb animation just stay paused
            //will switch to idle tho
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }
    void Run()
    {           //velocity.y so what is my y velocity (none) keep it //so he will move only left/right
        Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity= playerVelocity;//if we use only this character will be moving up down as well

        //my naimator *explainatory, SetBool (bcs true/false
        //string reference bcs how is it called in animator in unity
        //true bcs yes it is running

        //better if not hardcoded but with variable so if speed >zero is moving do naimation IsRunning
        //if its not then not
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning",playerHasHorizontalSpeed);
    }
    void FlipSprite()
    {  
        //this will make sure that when standing it wont flip
        //its if my speed is 0 literaly
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon;


        if (playerHasHorizontalSpeed)//ifp layer has (some) horizontal speed
        {
            //we need that signe and dont want to change anything on axis y
            //1f because it is 1 in scale in transform
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void Die()
    { 
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards", "Water"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("IsDead");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        
        { 
            //spawn bullet, where, which direction its facing
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }
}
