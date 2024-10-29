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
    private Vector3 movingplat;

    public Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        right = false;
        left = false;
        jump = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if ((Input.GetButtonDown("Jump") || jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", true);

            audioSource.PlayOneShot(jump_audio);
        }                  

        MovePlayer();
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
        // Update the Rigidbody velocity for horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Set animator parameters
        animator.SetFloat("speed", Mathf.Abs(horizontalInput * moveSpeed));
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingGround"))
        {
            Vector3 platformMovement = other.transform.position - movingplat;
            transform.position += platformMovement;
        }

        if (other.gameObject.CompareTag("MovingGround"))
        {
            movingplat = other.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingGround") || other.gameObject.CompareTag("SpawnPlatform"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);

            if (other.gameObject.CompareTag("MovingGround"))
            {
                movingplat = other.transform.position;
            }

        }
}

        private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingGround") || other.gameObject.CompareTag("SpawnPlatform"))
        {
            isGrounded = true;
        }
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

    public void PointerUpRight()
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
