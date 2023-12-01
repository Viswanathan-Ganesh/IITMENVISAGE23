using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void FixedUpdate()
    {
        Vector2 positionVec = playerTr.position - transform.position;
        float distance = positionVec.magnitude;

        if (distance <= enemyVisionDistance && distance >= enemyAttackRange && isDodging == false)
        {
            Vector2 VelocityDirection = (positionVec) / positionVec.magnitude;
            rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, 0f);
        }
        else if (isDodging == false)
        {
            rb.velocity = Vector2.zero;
        }

        if (distance <= playerTr.GetComponent<PlayerMoves>().normalRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Dodge(playerTr.GetComponent<PlayerMoves>().facingDir);
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

    void Dodge(Vector2 facingDirection)
    {
        rb.AddForce(facingDirection * dodgeForce * Time.deltaTime, ForceMode2D.Impulse);
        isDodging = true;
        dodgeTimer = dodgeCooldown;
        Debug.Log("HI");
    }
}
