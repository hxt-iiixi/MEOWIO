using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float moveInput;

    public float jump;
    public bool isJumping = true;
    private Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
            Debug.Log("jump");
            animator.SetBool("isJumping", !isJumping);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isJumping = true;
        animator.SetBool("isJumping", !isJumping);
    }
}

