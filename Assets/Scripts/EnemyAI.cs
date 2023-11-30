using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public float enemyVisionDistance;
    public float enemySpeed;
    public Transform playerTr;

    public LayerMask playerLayer;
    public float enemyAttackRange;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 positionVec = playerTr.position - transform.position;
        float distance = positionVec.magnitude;

        if (distance <= enemyVisionDistance && distance >= enemyAttackRange)
        {
            Vector2 VelocityDirection = (positionVec) / positionVec.magnitude;

            rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, 0f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        } 
    }
}
