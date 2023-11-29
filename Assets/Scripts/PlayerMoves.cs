using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMoves : MonoBehaviour
{
    public GameObject hitChecker;
    public float range;
    public Vector2 dir;
    public Vector3 offset;
    public Vector2 knockback;
    public float damage;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayStart = transform.position + offset; // creating a raycast
            RaycastHit2D hit = Physics2D.Raycast(rayStart, dir);
            Debug.DrawRay(rayStart, dir);
            float dis = Mathf.Abs(hit.point.x - rayStart.x);

            if (hit.collider.tag == "Enemy")
            {
                if (dis < range) 
                {
                    hit.collider.GetComponent<EnemyHealth>().SetHealth(damage);
                }
                else
                {
                    hit.collider.attachedRigidbody.AddForce(knockback * Time.deltaTime, ForceMode2D.Impulse);
                }
            }
            else
            {
                Debug.Log("Noone");
            }
        }
        
    }
}
