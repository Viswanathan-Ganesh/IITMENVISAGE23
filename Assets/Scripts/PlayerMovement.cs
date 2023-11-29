using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 jumpForce;
    public Vector2 doubleJumpForce;
    public Vector2 dashForce;

    public Vector3 speed;

    public bool isGrounded;
    public bool doubleJumpCapable = true;
    public bool dashCapable;
    public bool facingRight;

    public float doubleJumpRefillTime;
    private float DJTimer;
    private float dashTimer;
    public float dashRefillTime;
    
    
    void FixedUpdate()
    {
        // Getting Position 
        Vector3 Pos = transform.position;

        // Moving Forward
        if (Input.GetKey(KeyCode.D))
        {
            // For getting direction
            facingRight = true;
            // Smoothly interpolating between two points by a small change t
            transform.position = Vector2.Lerp(transform.position, Pos += speed * Time.deltaTime, Time.deltaTime);
        }


        // Moving Backward
        if (Input.GetKey(KeyCode.A))
        {
            // For getting direction
            facingRight = false;
            // Smoothly interpolating between two points by a small change t
            transform.position = Vector2.Lerp(transform.position, Pos -= speed * Time.deltaTime, Time.deltaTime);
        }

        // Jump
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded) // Checking if the player is grounded or not
        {
            rb.AddForce(jumpForce * Time.deltaTime, ForceMode2D.Impulse); // Applying an impulse
            isGrounded = false; // Setting it false so that user cant jump infinitely
        }

        // Double Jump
        if (Input.GetKey(KeyCode.LeftControl) && !isGrounded && doubleJumpCapable) // Checks if DJ capable and if the player is already mid-air
        {
            rb.AddForce(doubleJumpForce * Time.deltaTime, ForceMode2D.Impulse); // applies impules
            DJTimer = doubleJumpRefillTime; // giving charging time
            doubleJumpCapable = false; // setting it to false so no inf use
        }


        // Dash
        if (Input.GetKey(KeyCode.LeftShift) && dashCapable) // checking dashcapability
        {
            if (facingRight)
            {
                rb.AddForce(dashForce * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(-dashForce * Time.deltaTime, ForceMode2D.Impulse);
            }
            dashTimer = dashRefillTime; // Setting refill time
            dashCapable = false;
        }

        // DJ Timer
        if (doubleJumpCapable == false)
        {
            DJTimer -= Time.deltaTime;
        }
        if (DJTimer <= 0f)
        {
            doubleJumpCapable = true;
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
    }
}
