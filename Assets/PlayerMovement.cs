using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 speed;
    private bool isGrounded;
    public Rigidbody2D rb;
    public Vector2 jumpForce;
    public Vector2 doubleJumpForce;
    public bool doubleJumpCapable;
    public float doubleJumpRefillTime;
    private float DJTimer;
    private float dashTimer;
    public bool dashCapable;
    public float dashRefillTime;
    public Vector2 dashForce;

    public bool facingRight;


    void FixedUpdate()
    {

        Vector3 Pos = transform.position;
        if (Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            transform.position = Vector2.Lerp(transform.position, Pos += speed * Time.deltaTime, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            transform.position = Vector2.Lerp(transform.position, Pos -= speed * Time.deltaTime, Time.deltaTime);
        }


        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded)
        {
            rb.AddForce(jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            doubleJumpCapable = true;
            isGrounded = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && !isGrounded && doubleJumpCapable)
        {
            rb.AddForce(doubleJumpForce * Time.deltaTime, ForceMode2D.Impulse);
            DJTimer = doubleJumpRefillTime;
            doubleJumpCapable = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && dashCapable)
        {
            if (facingRight)
            {
                rb.AddForce(dashForce * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(-dashForce * Time.deltaTime, ForceMode2D.Impulse);
            }
            dashTimer = dashRefillTime;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
    }
}
