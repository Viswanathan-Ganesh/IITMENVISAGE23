using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 jumpForce;
    public Vector2 doubleJumpForce;
    public Vector2 dashForce;

    public Vector3 speed;


    public bool doubleJumpCapable = true;
    public bool dashCapable;
    public bool facingRight;

    public float doubleJumpRefillTime;
    private float DJTimer;
    private float dashTimer;
    public float dashRefillTime;

    public float jumpSpeed;
    private int maxJumps = 1;
    private int jumpsLeft;
    public float dashSpeed;

    public float dashPeriod;
    private float dashPeriodTimer = 0f;

    public bool isLanded;
    public Vector2 playerSize;
    public float castDistance; 
    public LayerMask platformLayer;

    public Animator playerAnimator;
    private void Start()
    {
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        // Getting Position 
        Vector3 Pos = transform.position;
        playerAnimator.SetBool("isRunning", false);

        // Moving Forward
        if (Input.GetKey(KeyCode.D))
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("isRunning", true);
            // For getting direction
            facingRight = true;
            // Smoothly interpolating between two points by a small change t
            transform.position = Vector2.Lerp(transform.position, Pos += speed * Time.deltaTime, Time.deltaTime);
        }


        // Moving Backward
        if (Input.GetKey(KeyCode.A))
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            playerAnimator.SetBool("isRunning", true);
            // For getting direction
            facingRight = false;
            // Smoothly interpolating between two points by a small change t
            transform.position = Vector2.Lerp(transform.position, Pos -= speed * Time.deltaTime, Time.deltaTime);
        }

        

        // Jump
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0) // Checking if the player is grounded or not
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
            jumpsLeft -= 1;
        }

        if (isGrounded())
        {
            jumpsLeft = maxJumps;
        } //refill

        // Double Jump
        /*
        if (Input.GetKey(KeyCode.LeftControl) && !isGrounded() && doubleJumpCapable) // Checks if DJ capable and if the player is already mid-air
        {
            rb.AddForce(doubleJumpForce * Time.deltaTime, ForceMode2D.Impulse); // applies impules

        }
        */


        // Dash
        if (Input.GetKey(KeyCode.LeftShift) && dashCapable) // checking dashcapability
        {
            if (facingRight)
            {
                //rb.AddForce(dashForce * Time.deltaTime, ForceMode2D.Impulse);
                rb.velocity = new Vector2(dashSpeed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, 0f);
                //rb.AddForce(-dashForce * Time.deltaTime, ForceMode2D.Impulse);
            }
            dashPeriodTimer = dashPeriod;
            dashTimer = dashRefillTime; // Setting refill time
            dashCapable = false;
        }


        if (dashPeriodTimer < 0f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            dashPeriodTimer = 0f;
        }

        if (dashPeriodTimer  > 0f)
        {
            dashPeriodTimer -= Time.deltaTime;
        }
        // Dash Timer
        if (dashCapable == false)
        {
            
            dashTimer -= Time.deltaTime;
        }
        if (dashTimer <= 0f)
        {
            dashCapable = true;

        }
    }

    // Checks if the player is grounded
    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, playerSize, 0, -transform.up, castDistance, platformLayer))
        {
            isLanded = true;
            return true;
        }

        else
        {
            isLanded = false;
            return false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, playerSize);
    }


}