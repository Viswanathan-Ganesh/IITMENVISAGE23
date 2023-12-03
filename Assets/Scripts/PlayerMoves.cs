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

    public Vector2 dir = new Vector2(1f, 0f);
    public Vector2 facingDir;
    public Vector3 offset;
    public Vector3 offset1;
    public Vector2 critBack;
    public Vector2 knockBack;
    
    public bool canAttack = true;
    public float attackCooldown;
    private float attackTimer;

    public bool isCriting;
    public float slashDamage;
    public bool canSlash = true;
    public float slashRange;
    public Transform slashPoint;
    public Vector2 slashPointPos;
    public float slashCoolDown;
    private float slashTimer;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            facingDir = new Vector2(1f, 0f);
            offset1 = offset;
            slashPointPos = transform.position + new Vector3(2f, 0f, 0f);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            facingDir = new Vector2(-1f, 0f);
            offset1 = -offset;
            slashPointPos = transform.position + new Vector3(-2f, 0f, 0f);
        }

        // Normal Attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Vector2 rayStart = transform.position + offset1; // creating start point for the raycast
            RaycastHit2D hit = Physics2D.Raycast(rayStart, facingDir); // making a raycast 
            Debug.DrawRay(rayStart, facingDir);
            
            if (hit.collider != null) // checking if the collider hits nothing
            {
                if (hit.collider.tag == "Enemy")
                {
                    float dis = Mathf.Abs(hit.point.x - rayStart.x); // finding distance at which it hit an object
                    if (dis <= critRange && gameObject.GetComponent<PlayerMovement>().isLanded == false)
                    {
                        isCriting = true;
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
        if (Input.GetKeyDown(KeyCode.S) && canSlash)
        {
            Slash();
            canSlash = false;
            slashTimer = slashCoolDown;
        }

        // attack cooldown
        if (attackTimer >= 0f && canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }

        // slash cooldown
        if (slashTimer >= 0f && canSlash == false)
        {
            slashTimer -= Time.deltaTime;
        }
        else
        {
            canSlash = true;
        }
        
    }

    void Slash()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashPointPos, slashRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
            enemy.GetComponent<EnemyHealth>().SetHealth(slashDamage);

    }

    void OnDrawGizmos()
    {
        if (slashPoint == null)
            return;

        Gizmos.DrawWireSphere(slashPointPos, slashRange);
    }
}
