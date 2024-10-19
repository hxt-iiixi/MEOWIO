using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catscript : MonoBehaviour
{
    public float horizontalInput;
    public float moveSpeed = 3f;
    bool isFacingRight = false;
    public float jumpPower = 5f;
    bool isGrounded = false;
    private bool right;
    private bool left;
    private bool jump;

    public AudioSource audioSource;
    public AudioClip jump_audio;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        right = false;
        left = false;
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if ((Input.GetButtonDown("Jump") || jump) && isGrounded)  
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            
            if (audioSource != null && jump_audio != null)
            {
                audioSource.PlayOneShot(jump_audio);
            }
        }

        MovePlayer();

    }

    public void Superj(float value)
    {
        jumpPower += value;
    }


    private void MovePlayer()
    {
        if (left)
        {
            horizontalInput = -moveSpeed;

        }
        else if (right)
        {
            horizontalInput = moveSpeed;
        }
        else
        {
            horizontalInput = 0;
        }

    }
        private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", (rb.velocity.y));
    }
    void FlipSprite()

    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    public void PointerDownLeft()
    {
        left = true;
    }

    public void PointerUpLeft()
    {
        left = false;
    }

    public void PointerDownRight()
    {
        right = true;
    }

    public void PointerUprRght()
    {
        right = false;
    }
    public void PointerDownJump()
    {
        jump = true;
    }

    public void PointerUpJump()
    {
        jump = false;
    }
}

