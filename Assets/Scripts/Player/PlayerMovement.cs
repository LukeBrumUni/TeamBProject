using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;
    private bool isFacingRight;
    
    

    //References
    Rigidbody2D rb;
    PlayerStats player;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        isFacingRight = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastMovedVector = new Vector2(1, 0f); //If we don't do this and game starts up and don't move, the projectile weapon will have no momentum
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        if (StateManager.instance.isGameOver || StateManager.instance.currentState == StateManager.GameState.Paused)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    //Last moved X
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);  //Last moved Y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);  //While moving
        }

        if(!isFacingRight && moveX > 0f)
        {
            Flip();
        }
        else if (isFacingRight && moveX < 0f)
        {
            Flip();
        }
    }

    void Move()
    {
        if (StateManager.instance.isGameOver)
        {
            return;
        }

        rb.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
        animator.SetFloat("xVelocity", rb.velocity.magnitude); // use Math.Abs(rb.velocity.x) or .y if wanting to do directional sprites
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}