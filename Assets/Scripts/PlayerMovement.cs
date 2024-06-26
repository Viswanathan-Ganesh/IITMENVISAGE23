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

    private bool dPressed;
    private bool aPressed;

    public float playerJumpAnimationTime;
    private float playerJumpAnimationTimer;

    public Animator playerAnimator;

    public bool isfalling;
    public bool facingRight;
    private void Start()
    {
        jumpsLeft = maxJumps;
        facingRight = true;
    }

    void Update()
    {
        // Getting Position 
        Vector3 Pos = transform.position;
        playerAnimator.SetBool("isRunning", false);

        float movex = Input.GetAxisRaw("Horizontal");
       

        // Moving Forward
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
        
        rb.velocity = new Vector2(speed.x, rb.velocity.y);

        }

        

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
     
            rb.velocity = new Vector2(-speed.x, rb.velocity.y);
        }


        // Jump
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0) // Checking if the player is grounded or not
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            playerAnimator.SetTrigger("isjumping");
            
            jumpsLeft -= 1;
        }
       
        if (isGrounded())
        {
            jumpsLeft = maxJumps;

        } 


        if (Input.GetKey(KeyCode.LeftShift) && dashCapable) // checking dashcapability
        {
            if (facingRight)
            {
                //rb.AddForce(dashForce * Time.deltaTime, ForceMode2D.Impulse);
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                playerAnimator.SetTrigger("dashing");
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
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


        if (dashCapable == false)
        {
            
            dashTimer -= Time.deltaTime;
        }
        if (dashTimer <= 0f)
        {
            dashCapable = true;

        }

        isfalling = isjumping();

        playerAnimator.SetFloat("Speed", Mathf.Abs(movex));

        if ((movex < 0 && facingRight) || (movex > 0 && !facingRight))
        {
            facingRight = !facingRight;
            this.transform.Rotate(0, 180, 0);
        }


    }

    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, playerSize, 0, -transform.up, castDistance, platformLayer))
        {
            isLanded = true;
            playerAnimator.SetBool("isgrounded", true);
            return true;
        }

        else
        {
            isLanded = false;
            playerAnimator.SetBool("isgrounded", false);
            return false;
        }
    }

    private bool isjumping() 
    {
        if (rb.velocity.y < 0f)
        {
            
            playerAnimator.SetBool("isfalling", true);
            return true;
        }

        else 
        {
            playerAnimator.SetBool("isfalling", false);
            return false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, playerSize);
    }


}