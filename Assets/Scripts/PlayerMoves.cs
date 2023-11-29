using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMoves : MonoBehaviour
{
    
    public float critRange;
    public float normalRange;
    public float normalDamage;
    public float critDamage;
    
    public Vector2 dir;
    public Vector3 offset;
    public Vector2 critBack;
    public Vector2 knockBack;
    
    public bool canAttack = true;
    public float attackCooldown;
    private float attackTimer;

    void Update()
    {
        // Normal Attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Vector2 rayStart = transform.position + offset; // creating start point for the raycast
            RaycastHit2D hit = Physics2D.Raycast(rayStart, dir); // making a raycast 
            Debug.DrawRay(rayStart, dir);
            
            if (hit.collider != null) // checking if the collider hits nothing
            {
                if (hit.collider.tag == "Enemy")
                {
                    float dis = Mathf.Abs(hit.point.x - rayStart.x); // finding distance at which it hit an object
                    if (dis <= critRange && gameObject.GetComponent<PlayerMovement>().isGrounded == false)
                    {
                        hit.collider.GetComponent<EnemyHealth>().SetHealth(critDamage); // gets the componenet EnemyHealth from the collider it hit and calls the function SetHealth
                        hit.collider.attachedRigidbody.AddForce(critBack * Time.deltaTime, ForceMode2D.Impulse); // adding critBack
                    }
                    else if (dis <= normalRange && dis > critRange)
                    {
                        hit.collider.GetComponent<EnemyHealth>().SetHealth(normalDamage); // gets the componenet EnemyHealth from the collider it hit and calls the function SetHealth
                        hit.collider.attachedRigidbody.AddForce(knockBack * Time.deltaTime, ForceMode2D.Impulse); // adding knockback
                    }
                }
            }

            canAttack = false;
            attackTimer = attackCooldown;
        }

        // Slash Attack

        // attack cooldown
        if (attackTimer >= 0f && canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
        
    }
}
