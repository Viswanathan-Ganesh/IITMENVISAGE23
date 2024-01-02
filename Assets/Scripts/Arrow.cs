using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float timeTillDestroy;
    private float timer = 0f;
    

    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        
        if(timer < 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().SetHealth(Mathf.RoundToInt(damage * Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x)));
                collision.gameObject.GetComponent<Animator>().SetBool("isHurt", true);
                Destroy(gameObject);
            }
            else if(collision.tag != "Player")
            {
                timer = timeTillDestroy;
            }
            else if(collision.tag == "Stone")
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
