using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public float enemyVisionDistance;
    public float enemySpeed;
    public Transform playerTr;

    public float dodgeForce;
    public LayerMask playerLayer;
    public float enemyAttackRange;
    public bool isDodging = false;

    public float dodgeCooldown;
    private float dodgeTimer;
    public int level;


    void FixedUpdate()
    {
        Vector2 positionVec = transform.position - playerTr.position;
        float distance = positionVec.magnitude;
        Vector2 VelocityDirection = -(positionVec) / positionVec.magnitude;
        Vector2 vel = new Vector2(enemySpeed * VelocityDirection.x, 0f);

        if (distance <= enemyVisionDistance && distance > enemyAttackRange + 0.2f && isDodging == false)
        {
            rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, rb.velocity.y);
        }
        else if (distance < enemyAttackRange - 0.2f && distance >= 0f && isDodging == false)
        {
            rb.velocity = new Vector2(-enemySpeed * VelocityDirection.x, rb.velocity.y);
        }
        else if(isDodging == false)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }


        if (distance <= playerTr.GetComponent<PlayerMoves>().normalRange)
        {
            Vector2 facingDirection = playerTr.GetComponent<PlayerMoves>().facingDir;
            float relX = positionVec.x / Mathf.Abs(positionVec.x);
            bool grounded = playerTr.GetComponent<PlayerMovement>().isLanded;


            if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) || playerTr.GetComponent<PlayerMoves>().isCriting) && relX == facingDirection.x)
            {
                Dodge(facingDirection, level);
                playerTr.GetComponent<PlayerMoves>().isCriting = false;
            }
        }
        // Timer

        if (isDodging == true)
        {
            dodgeTimer -= Time.deltaTime;
        }
        if (dodgeTimer <= 0f)
        {
            isDodging = false;
        }
    }

    void Dodge(Vector2 facingDirection, int level) 
    {
        if (Randomized(level))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(facingDirection * dodgeForce * Time.deltaTime, ForceMode2D.Impulse);
            isDodging = true;
            dodgeTimer = dodgeCooldown;
            Debug.Log("HI");
        }
    }

    bool Randomized(int Level)
    {
        if (level == 1)
        {
            // 50% possiblity
            int rand = Random.Range(1, 2);

            if (rand == 1)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else if (level == 2)
        {
            // 50 % possiblity
            int rand = Random.Range(1, 4);

            if (rand == 1 || rand == 2 || rand == 3)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else if (level == 3)
        {
            // 90 % possiblity
            int rand = Random.Range(1, 10);

            if (rand == 1 || rand == 2 || rand == 3 || rand == 4 || rand == 5 || rand == 6 || rand == 7 || rand == 8 || rand == 9)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else
        {
            isDodging = false;
            return false;
        }
    }
}
